namespace KYC.TrueFace.Core.Domain.Options;

public class RekognitionOptions
{
    public float AutoApproveThreshold { get; init; } = 90f;

    /// <summary>
    /// Similarity (0-100) from which the result is no longer trusted as a mismatch: between this
    /// value and <see cref="AutoApproveThreshold"/> the onboarding is held for manual review.
    /// Below it the onboarding is denied automatically.
    /// </summary>
    public float ManualReviewThreshold { get; init; } = 30f;

    public int TimeoutSeconds { get; init; } = 30;

    public int MaxErrorRetry { get; init; } = 2;
}
