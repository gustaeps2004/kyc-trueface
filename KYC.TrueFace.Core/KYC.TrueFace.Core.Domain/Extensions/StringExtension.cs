namespace KYC.TrueFace.Core.Domain.Extensions;

public static class StringExtension
{
    public static string JustNumbers(this string input)
    {
        if (string.IsNullOrEmpty(input)) 
            return input;

        var buffer = input.Length <= 256
            ? stackalloc char[input.Length]
            : new char[input.Length];

        int index = 0;

        foreach (char c in input)
        {
            if (char.IsLetterOrDigit(c))
                buffer[index++] = c;
        }

        return new string(buffer[..index]);
    }
}