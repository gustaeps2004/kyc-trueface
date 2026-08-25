using System.ComponentModel;

namespace KYC.TrueFace.Core.Domain.Enums;

public enum Permission
{
    [Description("Commun")] Commun = 1,
    [Description("Administrator")] Administrator = 2,
    [Description("Master")] Master = 3
}