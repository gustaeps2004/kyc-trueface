using KYC.TrueFace.Core.Domain.Entities.Base;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Domain.Entities;

public class User : EntityBase
{
    public Guid CodePartner { get; set; }
    public string Name { get; set; } = null!;
    public string IdNumber { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string? MotherName { get; set; }
    public string Email { get; set; } = null!;
    public Permission Permission { get; set; }
    public Situation Situation { get; set; }

    public virtual Partner? Partner { get; set; }

    public User() {  }
    public User(
        Guid codePartner,
        string name,
        string idNumber,
        DateTime birthDate,
        string? motherName,
        string email,
        Permission permission)
    {
        Code = Guid.NewGuid();
        InclusionDt = DateTime.UtcNow;
        CodePartner = codePartner;
        Name = name;
        IdNumber = idNumber;
        BirthDate = DateTime.SpecifyKind(birthDate, DateTimeKind.Utc);
        MotherName = motherName;
        Email = email;
        Permission = permission;
        Situation = Situation.Enabled;
    }
}