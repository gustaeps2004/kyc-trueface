using KYC.TrueFace.Core.Domain.Entities.Base;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Domain.Entities;

public class UserReport : EntityBase
{
    public const int MaxSituationMessageLength = 500;

    public Guid CodeUser { get; set; }
    public string? FilterText { get; set; }
    public Situation? FilterSituation { get; set; }
    public DateTime? FilterStartDt { get; set; }
    public DateTime? FilterEndDt { get; set; }
    public UserReportSituation Situation { get; set; }
    public string? SituationMessage { get; set; }
    public DateTime SituationDt { get; set; }
    public int AttemptCount { get; set; }

    public virtual User? User { get; set; }

    public UserReport() {  }
    public UserReport(
        Guid codeUser,
        string? filterText,
        Situation? filterSituation,
        DateTime? filterStartDt,
        DateTime? filterEndDt)
    {
        Code = Guid.NewGuid();
        InclusionDt = DateTime.UtcNow;
        CodeUser = codeUser;
        FilterText = filterText;
        FilterSituation = filterSituation;
        FilterStartDt = filterStartDt;
        FilterEndDt = filterEndDt;
        Situation = UserReportSituation.Pending;
        SituationDt = DateTime.UtcNow;
    }

    public void MarkAsProcessing()
    {
        Situation = UserReportSituation.Processing;
        SituationDt = DateTime.UtcNow;
        AttemptCount++;
    }

    public void MarkAsSent(string message)
    {
        Situation = UserReportSituation.Sent;
        SituationMessage = Truncate(message);
        SituationDt = DateTime.UtcNow;
    }

    public void MarkAsError(string message)
    {
        Situation = UserReportSituation.Error;
        SituationMessage = Truncate(message);
        SituationDt = DateTime.UtcNow;
    }

    private static string Truncate(string message)
        => message.Length <= MaxSituationMessageLength
            ? message
            : message[..(MaxSituationMessageLength - 3)] + "...";
}
