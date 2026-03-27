using KYC.TrueFace.ApiPartner.Entities.Base;
using KYC.TrueFace.ApiPartner.Enums;

namespace KYC.TrueFace.ApiPartner.Entities;

public class PartnerCredentials : EntityBase<Guid, int>
{
    public Guid CodePartner { get; set; }
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public required string GrantType { get; set; }
    public Situation Situation { get; set; }

    protected override void Validate() {  }
}