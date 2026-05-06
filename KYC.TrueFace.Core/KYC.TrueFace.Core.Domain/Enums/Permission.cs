using System.ComponentModel;

namespace KYC.TrueFace.Core.Domain.Enums;

public enum Permission
{
    [Description("Commun")] Commun = 1,
    [Description("Commun, Administrator")] Administrator = 2,
    [Description("Commun, Administrator, Master")] Master = 3
}