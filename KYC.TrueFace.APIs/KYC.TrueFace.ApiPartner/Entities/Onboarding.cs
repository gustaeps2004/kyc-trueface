using KYC.TrueFace.ApiPartner.Entities.Base;
using KYC.TrueFace.ApiPartner.Enums;

namespace KYC.TrueFace.ApiPartner.Entities;

public class Onboarding : EntityBase<Guid, int>
{
    public Guid CodePartner { get; set; }
    public DateTime InclusionDt { get; set; }
    public DateTime SituationDt { get; set; }
    public SituationOnboarding Situation { get; set; }
    public required string PathDocument { get; set; }
    public required string PathSelfie { get; set; }

    protected override void Validate() { }
}