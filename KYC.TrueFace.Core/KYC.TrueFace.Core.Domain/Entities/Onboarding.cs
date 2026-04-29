using KYC.TrueFace.Core.Domain.Entities.Base;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Domain.Entities;

public class Onboarding : EntityBase
{
    public Guid CodePartner { get; set; }
    public DateTime SituationDt { get; set; }
    public OnboardingSituation Situation { get; set; }
    public required string PathDocument { get; set; }
    public required string PathSelfie { get; set; }
}