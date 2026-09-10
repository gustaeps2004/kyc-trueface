using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Entities;
using KYC.TrueFace.Core.Domain.Exceptions;
using KYC.TrueFace.Core.Domain.Repositories;

namespace KYC.TrueFace.Core.Application.Services.Report;

public class UserReportService(
    IUserRepository userRepository,
    IUserReportRepository userReportRepository) : IUserReportService
{
    public async Task<Guid> RequestAsync(
        CreateUserReportDto reportDto,
        Guid codeUser,
        Guid codePartner,
        CancellationToken ct = default)
    {
        reportDto.Validate();

        var user = await userRepository.GetByCodeAsync(codeUser, ct)
                        ?? throw new KycException(ValidationErrors.UserNotExisted);

        if (user.CodePartner != codePartner)
            throw new KycException(ValidationErrors.UserNotExisted);

        var report = new UserReport(
                        user.Code,
                        reportDto.Filter,
                        reportDto.Situation,
                        reportDto.StartDt,
                        reportDto.EndDt
                    );

        userReportRepository.Insert(report);
        await userReportRepository.SaveChangesAsync(ct);

        return report.Code;
    }
}
