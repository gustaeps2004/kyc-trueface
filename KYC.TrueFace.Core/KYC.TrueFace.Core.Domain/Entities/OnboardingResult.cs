using KYC.TrueFace.Core.Domain.Entities.Base;

namespace KYC.TrueFace.Core.Domain.Entities;

public class OnboardingResult : EntityBase
{
    public Guid CodeOnboarding { get; set; }
    public Guid CodeUser { get; set; }
    public required string Observation { get; set; }
}