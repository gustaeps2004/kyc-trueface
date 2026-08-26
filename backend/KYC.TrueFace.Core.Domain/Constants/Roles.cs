namespace KYC.TrueFace.Core.Domain.Constants;

public static class Roles
{
    public const string Administrator = "ADMINISTRATOR";
    public const string Master = "MASTER";
    public const string AdministratorOrMaster = $"{Administrator},{Master}";
}
