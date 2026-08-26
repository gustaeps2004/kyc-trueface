using Konscious.Security.Cryptography;
using Microsoft.AspNet.Identity;
using System.Security.Cryptography;
using System.Text;

namespace KYC.TrueFace.Core.Application.Helpers;

public static class PasswordHelper
{
    private const int SaltSize = 16; // 128 bits
    private const int HashSize = 32; // 256 bits

    private const int Iterations = 3;
    private const int MemorySize = 65536; // 64 MB
    private const int Parallelism = 2;

    public static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

        byte[] hash = Hash(password, salt, Iterations, MemorySize, Parallelism);

        return FormatHash(hash, salt, Iterations, MemorySize, Parallelism);
    }

    public static bool IsValidPassword(string password, string storedHash)
    {
        var parsed = ParseHash(storedHash);

        byte[] computedHash = Hash(
            password,
            parsed.Salt,
            parsed.Iterations,
            parsed.MemorySize,
            parsed.Parallelism);

        return CryptographicOperations.FixedTimeEquals(computedHash, parsed.Hash);
    }

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

    private static byte[] Hash(
        string password, 
        byte[] salt, 
        int iterations, 
        int memorySize, 
        int parallelism)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            Iterations = iterations,
            MemorySize = memorySize,
            DegreeOfParallelism = parallelism
        };

        return argon2.GetBytes(HashSize);
    }

    private static string FormatHash(byte[] hash, byte[] salt, int iterations, int memorySize, int parallelism)
    {
        return $"$argon2id$v=1$m={memorySize},t={iterations},p={parallelism}$" +
               $"{Convert.ToBase64String(salt)}$" +
               $"{Convert.ToBase64String(hash)}";
    }

    private static (byte[] Salt, byte[] Hash, int Iterations, int MemorySize, int Parallelism) ParseHash(string hash)
    {
        var parts = hash.Split('$', StringSplitOptions.RemoveEmptyEntries);

        var paramPart = parts[2].Split(',');

        int memory = int.Parse(paramPart[0].Split('=')[1]);
        int iterations = int.Parse(paramPart[1].Split('=')[1]);
        int parallelism = int.Parse(paramPart[2].Split('=')[1]);

        byte[] salt = Convert.FromBase64String(parts[3]);
        byte[] hashBytes = Convert.FromBase64String(parts[4]);

        return (salt, hashBytes, iterations, memory, parallelism);
    }
}