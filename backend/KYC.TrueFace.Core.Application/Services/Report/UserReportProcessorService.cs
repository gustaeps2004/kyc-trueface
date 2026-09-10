using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Application.Services.Email;
using KYC.TrueFace.Core.Application.Services.Email.Templates;
using KYC.TrueFace.Core.Application.Services.User;
using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Entities;
using KYC.TrueFace.Core.Domain.Exceptions;
using KYC.TrueFace.Core.Domain.Options;
using KYC.TrueFace.Core.Domain.Repositories;
using Microsoft.Extensions.Options;

namespace KYC.TrueFace.Core.Application.Services.Report;

public class UserReportProcessorService(
    IUserReportRepository userReportRepository,
    IUserRepository userRepository,
    IUserReportExcelGenerator excelGenerator,
    IEmailService emailService,
    IOptions<UserReportOptions> reportOptions) : IUserReportProcessorService
{
    public async Task ProcessPendingAsync(CancellationToken ct = default)
    {
        var options = reportOptions.Value;
        var staleBefore = DateTime.UtcNow.AddMinutes(-options.StaleProcessingMinutes);

        var claimed = await userReportRepository.ClaimPendingAsync(options.BatchSize, staleBefore, ct);

        foreach (var report in claimed)
        {
            if (ct.IsCancellationRequested)
                break;

            if (report.AttemptCount > options.MaxAttempts)
            {
                report.MarkAsError($"Max attempts ({options.MaxAttempts}) exceeded.");
            }
            else
            {
                try
                {
                    report.MarkAsSent(await SendAsync(report, ct));
                }
                catch (OperationCanceledException) when (ct.IsCancellationRequested)
                {
                    break;
                }
                catch (Exception ex)
                {
                    report.MarkAsError($"{ex.GetType().Name}: {ex.Message}");
                }
            }

            userReportRepository.Update(report);

            await userReportRepository.SaveChangesAsync(CancellationToken.None);
        }
    }

    private async Task<string> SendAsync(UserReport report, CancellationToken ct)
    {
        var requester = await userRepository.GetByCodeAsync(report.CodeUser, ct)
                            ?? throw new KycException(ValidationErrors.UserNotExisted);

        var users = await userRepository.ListForReportAsync(
                        requester.CodePartner,
                        report.FilterSituation,
                        report.FilterStartDt,
                        report.FilterEndDt?.AddDays(1),
                        ct);

        var filtered = users
                        .Where(u => UserFilter.MatchesText(u.Name, u.IdNumber, u.Email, report.FilterText))
                        .ToList();

        var content = excelGenerator.Generate(filtered);
        var generatedAt = DateTime.UtcNow;
        var fileName = $"{ReportDefaults.UserReportFileNamePrefix}-{generatedAt:yyyyMMdd-HHmm}.xlsx";

        await emailService.SendAsync(
            new SendEmailDto(
                requester.Email,
                UserReportEmailTemplate.Subject,
                UserReportEmailTemplate.Ready(requester.Name, filtered.Count, generatedAt),
                new EmailAttachmentDto(fileName, content, ReportDefaults.XlsxContentType)
            ),
            ct
        );

        return $"Sent to {requester.Email} - {filtered.Count} row(s), file {fileName}.";
    }
}
