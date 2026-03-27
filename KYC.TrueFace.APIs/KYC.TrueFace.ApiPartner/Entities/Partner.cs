using KYC.TrueFace.ApiPartner.Entities.Base;
using KYC.TrueFace.ApiPartner.Enums;

namespace KYC.TrueFace.ApiPartner.Entities;

public class Partner : EntityBase<Guid, int>
{
    public required string IdNumber { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public DateTime InclusionDt { get; set; }
    public Situation Situation { get; set; }
    protected override void Validate() { }
}