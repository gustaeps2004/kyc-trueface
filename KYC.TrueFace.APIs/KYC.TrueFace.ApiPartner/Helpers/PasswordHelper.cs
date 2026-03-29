using Konscious.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using System.Security.Cryptography;
using System.Text;

namespace KYC.TrueFace.ApiPartner.Helpers;

public static class PasswordHelper
{
    public static bool IsStrong(string value)
    {
        if (value.Length < 8)
            return false;

        bool hasUpper = false;
        bool hasLower = false;
        bool hasDigit = false;
        bool hasSpecial = false;

        for (int i = 0; i < value.Length; i++)
        {
            char c = value[i];

            if (!hasUpper && char.IsUpper(c))
                hasUpper = true;
            else if (!hasLower && char.IsLower(c))
                hasLower = true;
            else if (!hasDigit && char.IsDigit(c))
                hasDigit = true;
            else if (!hasSpecial && !char.IsLetterOrDigit(c))
                hasSpecial = true;

            if (hasUpper && hasLower && hasDigit && hasSpecial)
                return true;
        }

        return false;
    }

    public static PasswordVerificationResult VerifyPassword(string password, byte[] storedHash)
    {
        var hash = Hash(password);
        bool isValid = CryptographicOperations.FixedTimeEquals(hash, storedHash);

        return isValid
            ? PasswordVerificationResult.Success
            : PasswordVerificationResult.Failed;
    }

    public static byte[] Hash(string password)
    {
        var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Iterations = 2,
            MemorySize = 65536,
            DegreeOfParallelism = 2
        };

        return argon2.GetBytes(32);
    }
}