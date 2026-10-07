using System.Globalization;
using Amazon.Rekognition;
using Amazon.Rekognition.Model;
using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.Options;
using KYC.TrueFace.Core.Domain.ValueObjects;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KYC.TrueFace.Core.Application.Services.FaceComparison;

public class RekognitionFaceComparisonService(
    IAmazonRekognition rekognition,
    IOptions<AwsOptions> awsOptions,
    ILogger<RekognitionFaceComparisonService> logger) : IFaceComparisonService
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
                return Inconclusive(OnboardingMessages.NoFaceFound);

            return Decide(similarity.Value, options);
        }
        catch (InvalidParameterException ex)
        {
            // Raised when Rekognition cannot detect a face in the document image.
            return Rejected(OnboardingMessages.ImagesUnreadable, ex);
        }
        catch (InvalidImageFormatException ex)
        {
            return Rejected(OnboardingMessages.ImageFormatUnsupported, ex);
        }
        catch (ImageTooLargeException ex)
        {
            return Rejected(OnboardingMessages.ImageTooLarge, ex);
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
                new LocalizedMessage(OnboardingMessages.FacesMatched, new Dictionary<string, string>
                {
                    ["similarity"] = observed,
                    ["approveThreshold"] = approve
                }));

        if (similarity >= options.ManualReviewThreshold)
            return new FaceComparisonResultDto(
                FaceComparisonOutcome.ReviewRequired,
                similarity,
                new LocalizedMessage(OnboardingMessages.ManualReviewRequired, new Dictionary<string, string>
                {
                    ["similarity"] = observed,
                    ["reviewThreshold"] = review,
                    ["approveThreshold"] = approve
                }));

        return new FaceComparisonResultDto(
            FaceComparisonOutcome.NotMatched,
            similarity,
            new LocalizedMessage(OnboardingMessages.FacesNotMatched, new Dictionary<string, string>
            {
                ["similarity"] = observed,
                ["reviewThreshold"] = review
            }));
    }

    // Invariant culture so the recorded values read the same on any host locale.
    private static string Percent(double value)
        => value.ToString("F2", CultureInfo.InvariantCulture);

    // Rekognition's own wording is English-only and not actionable for the user, so it is
    // logged instead of being recorded on the onboarding.
    private FaceComparisonResultDto Rejected(string messageKey, AmazonRekognitionException ex)
    {
        logger.LogWarning(ex, "Rekognition rejected the images: {Detail}", ex.Message);

        return Inconclusive(messageKey);
    }

    private static FaceComparisonResultDto Inconclusive(string messageKey)
        => new(FaceComparisonOutcome.Inconclusive, null, new LocalizedMessage(messageKey));
}
