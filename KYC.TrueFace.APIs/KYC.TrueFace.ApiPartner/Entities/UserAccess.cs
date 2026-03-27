using KYC.TrueFace.ApiPartner.Entities.Base;
using KYC.TrueFace.ApiPartner.Enums;

namespace KYC.TrueFace.ApiPartner.Entities;

public class UserAccess : EntityBase<Guid, int>
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Role { get; set; }
    public required string Scope { get; set; }
    public DateTime InclusionDt { get; set; }
    public SituationAccess Situation { get; set; }

    protected override void Validate() {  }
}