using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Application.Messaging.Request;

public record CreateUserReportRequest
{
    public string? Filter { get; set; }
    public Situation? Situation { get; set; }
    public DateTime? StartDt { get; set; }
    public DateTime? EndDt { get; set; }

    public CreateUserReportDto ToDto()
        => new(
                Filter?.Trim(),
                Situation,
                StartDt,
                EndDt
            );
}
