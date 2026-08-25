using KYC.TrueFace.Core.Domain.Entities.Base;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Domain.Entities;

public class PartnerCredentials : EntityBase
{
    public Guid CodePartner { get; set; }
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public required string GrantType { get; set; }
    public Situation Situation { get; set; }

    public virtual Partner? Partner { get; set; }
}