namespace KYC.TrueFace.Core.Domain.Options;

public class LoginSecurityOptions
{
    public const string SectionName = "LoginSecurity";

    public int RateLimitPermitLimit { get; init; } = 5;

    public int RateLimitWindowSeconds { get; init; } = 60;

    public int MaxFailedAttempts { get; init; } = 5;

    public int LockoutMinutes { get; init; } = 15;
}
