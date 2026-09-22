using System.Globalization;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.Options;
using Microsoft.Extensions.Options;

namespace KYC.TrueFace.Core.Application.Services.FaceComparison;

public class RekognitionFaceComparisonService(
    IAmazonRekognition rekognition,
    IOptions<AwsOptions> awsOptions) : IFaceComparisonService
{
    public async Task<FaceComparisonResultDto> CompareAsync(
        byte[] documentImage,
        byte[] selfieImage,
        CancellationToken ct = default)
    {
        var options = awsOptions.Value.Rekognition;

        using var sourceStream = new MemoryStream(documentImage);
        using var targetStream = new MemoryStream(selfieImage);

        var request = new CompareFacesRequest
        {
            SourceImage = new Image { Bytes = sourceStream },
            TargetImage = new Image { Bytes = targetStream },
            // Ask Rekognition for every candidate and apply the thresholds here, so the
            // observed similarity is recorded even when the faces do not match.
            SimilarityThreshold = 0f,
            QualityFilter = QualityFilter.AUTO
        };

        try
        {
            var response = await rekognition.CompareFacesAsync(request, ct);

            var similarity = response.FaceMatches?
                                .Where(match => match.Similarity.HasValue)
                                .Max(match => (double?)match.Similarity!.Value);

            if (similarity is null)
                return Inconclusive("Rekognition found no comparable face in the selfie.");

            return Decide(similarity.Value, options);
        }
        catch (InvalidParameterException ex)
        {
            // Raised when Rekognition cannot detect a face in the document image.
            return Inconclusive($"Rekognition could not read the images: {ex.Message}");
        }
        catch (InvalidImageFormatException ex)
        {
            return Inconclusive($"Unsupported image format: {ex.Message}");
        }
        catch (ImageTooLargeException ex)
        {
            return Inconclusive($"Image too large for Rekognition: {ex.Message}");
        }
    }

    private static FaceComparisonResultDto Decide(double similarity, RekognitionOptions options)
    {
        var observed = Percent(similarity);
        var approve = Percent(options.AutoApproveThreshold);
        var review = Percent(options.ManualReviewThreshold);

        if (similarity >= options.AutoApproveThreshold)
            return new FaceComparisonResultDto(
                FaceComparisonOutcome.Matched,
                similarity,
                $"Faces matched with {observed}% similarity (auto-approve from {approve}%).");

        if (similarity >= options.ManualReviewThreshold)
            return new FaceComparisonResultDto(
                FaceComparisonOutcome.ReviewRequired,
                similarity,
                $"Similarity of {observed}% is between {review}% and {approve}% - manual review required.");

        return new FaceComparisonResultDto(
            FaceComparisonOutcome.NotMatched,
            similarity,
            $"Faces did not match: {observed}% similarity, below the {review}% minimum.");
    }

    // Invariant culture so the recorded message reads the same on any host locale.
    private static string Percent(double value)
        => value.ToString("F2", CultureInfo.InvariantCulture);

    private static FaceComparisonResultDto Inconclusive(string message)
        => new(FaceComparisonOutcome.Inconclusive, null, message);
}
