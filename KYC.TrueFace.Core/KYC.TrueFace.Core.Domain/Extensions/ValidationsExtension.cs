using System.Buffers;

namespace KYC.TrueFace.Core.Domain.Extensions;

public static class ValidationsExtension
{
    private static readonly SearchValues<char> SpecialChars =
        SearchValues.Create("!@#$%^&*()-_=+[]{}|;:',.<>?/`~");

    public static bool IsIdNumberInvalid(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf)) 
            return true;

        string numericCpf = new([.. cpf.Where(char.IsDigit)]);

        if (numericCpf.Length != 11) 
            return true;

        if (numericCpf.Distinct().Count() == 1) 
            return true;

        var multiplier1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplier2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = numericCpf[..9];
        int sum = 0;

        for (int i = 0; i < 9; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier1[i];

        int remainder = sum % 11;
        int digit1 = remainder < 2 ? 0 : 11 - remainder;

        tempCpf = tempCpf + digit1;
        sum = 0;

        for (int i = 0; i < 10; i++)
            sum += int.Parse(tempCpf[i].ToString()) * multiplier2[i];

        remainder = sum % 11;
        int digit2 = remainder < 2 ? 0 : 11 - remainder;

        return !numericCpf.EndsWith(digit1.ToString() + digit2.ToString());
    }

    public static bool IsValidPassword(string password)
    {
        if (string.IsNullOrEmpty(password) || password.Length < 8)
            return false;

        ReadOnlySpan<char> span = password.AsSpan();

        bool hasUpper = false;
        bool hasLower = false;
        bool hasDigit = false;
        bool hasSpecial = false;

        foreach (char c in span)
        {
            if (char.IsUpper(c)) 
                hasUpper = true;
            else if (char.IsLower(c)) 
                hasLower = true;
            else if (char.IsDigit(c)) 
                hasDigit = true;
            else if (SpecialChars.Contains(c)) 
                hasSpecial = true;

            if (hasUpper && hasLower && hasDigit && hasSpecial)
                return true;
        }

        return 
            hasUpper 
            && hasLower 
            && hasDigit 
            && hasSpecial;
    }
}