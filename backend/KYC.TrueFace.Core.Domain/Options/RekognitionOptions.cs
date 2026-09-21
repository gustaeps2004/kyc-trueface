namespace KYC.TrueFace.Core.Domain.Options;

public class RekognitionOptions
{
    /// <summary>Minimum similarity (0-100) for the two faces to be considered the same person.</summary>
    public float SimilarityThreshold { get; init; } = 90f;

    public int TimeoutSeconds { get; init; } = 30;

    public int MaxErrorRetry { get; init; } = 2;
}
