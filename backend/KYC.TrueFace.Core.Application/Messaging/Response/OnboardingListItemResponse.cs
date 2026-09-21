using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Application.Messaging.Response;

public class OnboardingListItemResponse
{
    public Guid Code { get; set; }
    public string IdNumber { get; set; } = null!;
    public string Name { get; set; } = null!;
    public DateTime InclusionDt { get; set; }
    public OnboardingSituation Situation { get; set; }
    public DateTime SituationDt { get; set; }
    public string? SituationMessage { get; set; }
    public double? Similarity { get; set; }
    public int AttemptCount { get; set; }
    /// <summary>Observation left by the reviewer, when the decision was taken by a human.</summary>
    public string? Observation { get; set; }
    public string PathDocument { get; set; } = null!;
    public string PathSelfie { get; set; } = null!;
}
