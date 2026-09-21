using System.Diagnostics.CodeAnalysis;
using KYC.TrueFace.Core.Domain.Entities.Base;

namespace KYC.TrueFace.Core.Domain.Entities;

public class OnboardingResult : EntityBase
{
    public Guid CodeOnboarding { get; set; }
    public Guid CodeUser { get; set; }
    public required string Observation { get; set; }

    public virtual Onboarding? Onboarding { get; set; }

    public OnboardingResult() { }

    [SetsRequiredMembers]
    public OnboardingResult(
        Guid codeOnboarding,
        Guid codeUser,
        string observation)
    {
        Code = Guid.NewGuid();
        InclusionDt = DateTime.UtcNow;
        CodeOnboarding = codeOnboarding;
        CodeUser = codeUser;
        Observation = observation;
    }
}