using KYC.TrueFace.Core.Domain.Entities.Base;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Domain.Entities;

public class UserAccess : EntityBase
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public Situation Situation { get; set; }
    public required string Role { get; set; }
    public required string Claim { get; set; }
}