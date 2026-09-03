using KYC.TrueFace.Core.Domain.Entities.Base;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Domain.Entities;

public class PartnerCredentials : EntityBase
{
    public Guid CodePartner { get; set; }
    public string ClientId { get; set; } = null!;

    // Must always be a hash (e.g. via IPasswordHasher.Hash), never the raw secret - see UserAccess.Password for the same convention.
    public string ClientSecret { get; set; } = null!;
    public string GrantType { get; set; } = null!;
    public Situation Situation { get; set; }

    public virtual Partner? Partner { get; set; }

    public PartnerCredentials() { }

    public PartnerCredentials(
        Guid codePartner,
        string clientId,
        string clientSecretHash,
        string grantType)
    {
        Code = Guid.NewGuid();
        InclusionDt = DateTime.UtcNow;
        CodePartner = codePartner;
        ClientId = clientId;
        ClientSecret = clientSecretHash;
        GrantType = grantType;
        Situation = Situation.Enabled;
    }
}