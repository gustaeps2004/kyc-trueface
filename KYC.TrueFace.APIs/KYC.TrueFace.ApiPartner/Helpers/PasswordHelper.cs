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
}