using KYC.TrueFace.Core.Application.Messaging.DTOs;

namespace KYC.TrueFace.Core.Application.Services.Report;

public interface IUserReportService
{
    Task<Guid> RequestAsync(
        CreateUserReportDto reportDto,
        Guid codeUser,
        Guid codePartner,
        CancellationToken ct = default);
}
