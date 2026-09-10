using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.Exceptions;

namespace KYC.TrueFace.Core.Application.Messaging.DTOs;

public class CreateUserReportDto(
    string? filter,
    Situation? situation,
    DateTime? startDt,
    DateTime? endDt)
{
    public string? Filter { get; set; } = filter;
    public Situation? Situation { get; set; } = situation;
    public DateTime? StartDt { get; set; } = NormalizeToUtcDate(startDt);
    public DateTime? EndDt { get; set; } = NormalizeToUtcDate(endDt);

    public void Validate()
    {
        if (Filter is not null && Filter.Length > ReportDefaults.UserReportFilterMaxLength)
            throw new KycException(ValidationErrors.UserReportFilterExceed);

        if (Situation is not null && !Enum.IsDefined(typeof(Situation), Situation.Value))
            throw new KycException(ValidationErrors.UserSituationInvalid);

        if (StartDt is not null && EndDt is not null && StartDt > EndDt)
            throw new KycException(ValidationErrors.UserReportInvalidPeriod);
    }

    private static DateTime? NormalizeToUtcDate(DateTime? value)
        => value is null
            ? null
            : DateTime.SpecifyKind(value.Value.Date, DateTimeKind.Utc);
}
