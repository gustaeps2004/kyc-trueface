using KYC.TrueFace.Core.Domain.Entities.Base;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Domain.Entities;

public class UserAccess : EntityBase
{
    public string Username { get; set; } = null!;
    public string Password { get; set; } = null!;
    public Situation Situation { get; set; }
    public string Role { get; set; } = null!;
    public string Claim { get; set; } = null!;
    public string? ResetPasswordTokenHash { get; set; }
    public DateTime? ResetPasswordTokenExpiresAt { get; set; }
    public int AccessFailedCount { get; set; }
    public DateTime? LockoutEndsAt { get; set; }

    public UserAccess() {  }
    public UserAccess(
        string username, 
        string password, 
        string role, 
        string claim)
    {
        Code = Guid.NewGuid();
        InclusionDt = DateTime.UtcNow;
        Username = username;
        Password = password;
        Situation = Situation.Enabled;
        Role = role;
        Claim = claim;
    }

    public void UpdatePassword(string password)
        => Password = password;

    public bool IsLockedOut(DateTime utcNow)
        => LockoutEndsAt is not null && LockoutEndsAt > utcNow;

    public void RegisterFailedLogin(int maxFailedAttempts, TimeSpan lockoutDuration)
    {
        AccessFailedCount++;

        if (maxFailedAttempts > 0 && AccessFailedCount >= maxFailedAttempts)
        {
            LockoutEndsAt = DateTime.UtcNow.Add(lockoutDuration);
            AccessFailedCount = 0;
        }
    }

    public void RegisterSuccessfulLogin()
    {
        AccessFailedCount = 0;
        LockoutEndsAt = null;
    }

    public void SetResetPasswordToken(string tokenHash, DateTime expiresAt)
    {
        ResetPasswordTokenHash = tokenHash;
        ResetPasswordTokenExpiresAt = expiresAt;
    }

    public void ClearResetPasswordToken()
    {
        ResetPasswordTokenHash = null;
        ResetPasswordTokenExpiresAt = null;
    }
}