namespace KYC.TrueFace.Core.Domain.Options;

public class UserReportOptions
{
    public const string SectionName = "UserReport";

    public int PollingIntervalSeconds { get; init; } = 30;

    public int BatchSize { get; init; } = 5;

    public int StaleProcessingMinutes { get; init; } = 15;

    public int MaxAttempts { get; init; } = 3;
}
