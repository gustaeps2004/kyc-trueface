namespace KYC.TrueFace.Core.Domain.Options;

public class OnboardingOptions
{
    public const string SectionName = "Onboarding";

    /// <summary>Root folder on the VPS where the uploaded images are stored.</summary>
    public string StoragePath { get; init; } = string.Empty;

    public int PollingIntervalSeconds { get; init; } = 5;

    public int BatchSize { get; init; } = 10;

    public int StaleProcessingMinutes { get; init; } = 5;

    public int MaxAttempts { get; init; } = 3;

    public long MaxImageSizeBytes { get; init; } = 5 * 1024 * 1024;

    public string[] AllowedContentTypes { get; init; } = ["image/jpeg", "image/png"];
}
