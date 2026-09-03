namespace KYC.TrueFace.Core.Application.Security;

public interface IPasswordHasher
{
    string Hash(string password);

    PasswordVerificationResult Verify(string password, string storedHash);

    Task<string> HashAsync(string password, CancellationToken ct = default);

    Task<PasswordVerificationResult> VerifyAsync(string password, string storedHash, CancellationToken ct = default);
}
