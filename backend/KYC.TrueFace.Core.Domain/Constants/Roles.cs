namespace KYC.TrueFace.Core.Domain.Constants;

public static class Roles
{
    public const string Commun = "COMMUN";
    public const string Administrator = "ADMINISTRATOR";
    public const string Master = "MASTER";
    public const string AdministratorOrMaster = $"{Administrator},{Master}";
    public const string AllAccess = $"{Commun},{Administrator},{Master}";
    public const string ResetPassword = "RESET_PASSWORD";
}
