using KYC.TrueFace.Core.Domain.Entities.Base;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Domain.Entities;

public class User : EntityBase
{
    public Guid CodePartner { get; set; }
    public required string Name { get; set; }
    public required string IdNumber { get; set; }
    public DateTime BirthDate { get; set; }
    public string? MotherName { get; set; }
    public required string Email { get; set; }
    public Permission Permission { get; set; }
    public Situation Situation { get; set; }
}