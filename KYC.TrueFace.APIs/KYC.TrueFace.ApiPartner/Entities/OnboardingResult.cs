using KYC.TrueFace.ApiPartner.Entities.Base;

namespace KYC.TrueFace.ApiPartner.Entities;

public class OnboardingResult : EntityBase<Guid, int>
{
    public Guid CodeOnboarding { get; set; }
    public Guid CodeUser { get; set; }
    public DateTime InclusionDt { get; set; }
    public required string Observation { get; set; }

    protected override void Validate() {  }
}