using System.Diagnostics.CodeAnalysis;
using KYC.TrueFace.Core.Domain.Entities.Base;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Domain.Entities;

public class Onboarding : EntityBase
{
    public const int MaxSituationMessageLength = 500;
    public const int MaxNameLength = 150;

    public Guid CodePartner { get; set; }
    public required string IdNumber { get; set; }
    public required string Name { get; set; }
    public DateTime SituationDt { get; set; }
    public OnboardingSituation Situation { get; set; }
    public required string PathDocument { get; set; }
    public required string PathSelfie { get; set; }
    public string? SituationMessage { get; set; }
    public double? Similarity { get; set; }
    public int AttemptCount { get; set; }

    public virtual Partner? Partner { get; set; }
    public virtual ICollection<OnboardingResult>? Results { get; set; }

    public Onboarding() { }

    [SetsRequiredMembers]
    public Onboarding(
        Guid code,
        Guid codePartner,
        string idNumber,
        string name,
        string pathDocument,
        string pathSelfie)
    {
        Code = code;
        InclusionDt = DateTime.UtcNow;
        CodePartner = codePartner;
        IdNumber = idNumber;
        Name = name;
        PathDocument = pathDocument;
        PathSelfie = pathSelfie;
        Situation = OnboardingSituation.Pending;
        SituationDt = DateTime.UtcNow;
    }

    public void MarkAsProcessing()
    {
        Situation = OnboardingSituation.Processing;
        SituationDt = DateTime.UtcNow;
        AttemptCount++;
    }

    public void MarkAsApproved(double? similarity, string message)
    {
        Situation = OnboardingSituation.Approved;
        Similarity = similarity;
        SituationMessage = Truncate(message);
        SituationDt = DateTime.UtcNow;
    }

    public void MarkAsDenied(double? similarity, string message)
    {
        Situation = OnboardingSituation.Denied;
        Similarity = similarity;
        SituationMessage = Truncate(message);
        SituationDt = DateTime.UtcNow;
    }

    /// <summary>Automatic validation failed or was inconclusive - a human has to decide.</summary>
    public void MarkForManualReview(string message)
    {
        Situation = OnboardingSituation.ManualReview;
        SituationMessage = Truncate(message);
        SituationDt = DateTime.UtcNow;
    }

    /// <summary>Transient failure: puts the record back in the queue for the next worker tick.</summary>
    public void MarkForRetry(string message)
    {
        Situation = OnboardingSituation.Pending;
        SituationMessage = Truncate(message);
        SituationDt = DateTime.UtcNow;
    }

    /// <summary>Decision taken by a human on a record the automatic validation could not settle.</summary>
    public void MarkAsManuallyReviewed(bool approved, string observation)
    {
        Situation = approved ? OnboardingSituation.Approved : OnboardingSituation.Denied;
        SituationMessage = Truncate(observation);
        SituationDt = DateTime.UtcNow;
    }

    private static string Truncate(string message)
        => message.Length <= MaxSituationMessageLength
            ? message
            : message[..(MaxSituationMessageLength - 3)] + "...";
}
