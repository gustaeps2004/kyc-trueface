using System.Security.Cryptography;
using System.Text;

namespace KYC.TrueFace.Core.Application.Helpers;

public static class PasswordHelper
{
    public static string GenerateStrongRandom()
    {
        const string upperData = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        const string lowerData = "abcdefghijklmnopqrstuvwxyz";
        const string numberData = "0123456789";
        const string specialData = "!@#$%^&*()_-+=[]{}|;:,.<>?";

        var password = new StringBuilder();
        password.Append(upperData[RandomNumberGenerator.GetInt32(upperData.Length)]);
        password.Append(lowerData[RandomNumberGenerator.GetInt32(lowerData.Length)]);
        password.Append(numberData[RandomNumberGenerator.GetInt32(numberData.Length)]);
        password.Append(specialData[RandomNumberGenerator.GetInt32(specialData.Length)]);

        string allChars = upperData + lowerData + numberData + specialData;
        for (int i = password.Length; i < 12; i++)
            password.Append(allChars[RandomNumberGenerator.GetInt32(allChars.Length)]);

        return new string(
            [..
                password
                .ToString()
                .ToCharArray()
                .OrderBy(s => RandomNumberGenerator.GetInt32(100))
            ]
        );
    }

    public static string GetSuffix(string username)
        => $"{username}_onb";

    public static string GenerateSecureToken()
        => Convert.ToHexString(RandomNumberGenerator.GetBytes(32));

    public static string HashToken(string token)
        => Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(token)));

    public static bool IsValidToken(string token, string? storedTokenHash, DateTime? expiresAt)
    {
        if (storedTokenHash is null || expiresAt is null || expiresAt < DateTime.UtcNow)
            return false;

        return CryptographicOperations.FixedTimeEquals(
            Encoding.UTF8.GetBytes(storedTokenHash),
            Encoding.UTF8.GetBytes(HashToken(token)));
    }
}
