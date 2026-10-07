using System.Diagnostics.CodeAnalysis;
using KYC.TrueFace.Core.Domain.Entities.Base;
using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.ValueObjects;

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
    /// <summary>Translation key for the automatic result, or the reviewer's own words once a human decided.</summary>
    public string? SituationMessage { get; set; }
    /// <summary>JSON with the values interpolated into <see cref="SituationMessage"/>, when it has any.</summary>
    public string? SituationMessageArgs { get; set; }
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

    public void MarkAsApproved(double? similarity, LocalizedMessage message)
    {
        Situation = OnboardingSituation.Approved;
        Similarity = similarity;
        SetSituationMessage(message);
        SituationDt = DateTime.UtcNow;
    }

    public void MarkAsDenied(double? similarity, LocalizedMessage message)
    {
        Situation = OnboardingSituation.Denied;
        Similarity = similarity;
        SetSituationMessage(message);
        SituationDt = DateTime.UtcNow;
    }

    /// <summary>
    /// Automatic validation failed, was inconclusive or landed in the uncertainty band -
    /// a human has to decide. <paramref name="similarity"/> is null when no score was produced.
    /// </summary>
    public void MarkForManualReview(LocalizedMessage message, double? similarity = null)
    {
        Situation = OnboardingSituation.ManualReview;
        Similarity = similarity;
        SetSituationMessage(message);
        SituationDt = DateTime.UtcNow;
    }

    /// <summary>Transient failure: puts the record back in the queue for the next worker tick.</summary>
    public void MarkForRetry(LocalizedMessage message)
    {
        Situation = OnboardingSituation.Pending;
        SetSituationMessage(message);
        SituationDt = DateTime.UtcNow;
    }

    /// <summary>Decision taken by a human on a record the automatic validation could not settle.</summary>
    public void MarkAsManuallyReviewed(bool approved, string observation)
    {
        Situation = approved ? OnboardingSituation.Approved : OnboardingSituation.Denied;
        SituationMessage = Truncate(observation);
        SituationMessageArgs = null;
        SituationDt = DateTime.UtcNow;
    }

    private void SetSituationMessage(LocalizedMessage message)
    {
        SituationMessage = message.Key;
        SituationMessageArgs = message.SerializeArgs();
    }

    private static string Truncate(string message)
        => message.Length <= MaxSituationMessageLength
            ? message
            : message[..(MaxSituationMessageLength - 3)] + "...";
}
