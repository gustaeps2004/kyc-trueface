using KYC.TrueFace.ApiPartner.Entities.Base;
using KYC.TrueFace.ApiPartner.Enums;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KYC.TrueFace.ApiPartner.Entities;

public class User : EntityBase<Guid, int>
{
    public Guid CodePartner { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string IdNumber { get; set; }
    public string? MotherName { get; set; }
    public DateOnly BirthDate { get; set; }
    public DateTime InclusionDt { get; set; }
    public Permission Permission { get; set; }
    public Situation Situation { get; set; }
    protected override void Validate() {  }
}