using KYC.TrueFace.Core.Application.Messaging.DTOs;

namespace KYC.TrueFace.Core.Application.Services.FaceComparison;

public interface IFaceComparisonService
{
    /// <summary>
    /// Compares the face on the document against the selfie. Provider errors that a retry
    /// cannot fix (no face found, unreadable image) come back as
    /// <see cref="Domain.Enums.FaceComparisonOutcome.Inconclusive"/>; transient failures throw.
    /// </summary>
    Task<FaceComparisonResultDto> CompareAsync(
        byte[] documentImage,
        byte[] selfieImage,
        CancellationToken ct = default);
}
