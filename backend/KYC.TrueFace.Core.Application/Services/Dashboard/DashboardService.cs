using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Domain.Repositories;

namespace KYC.TrueFace.Core.Application.Services.Dashboard;

public class DashboardService(IOnboardingRepository onboardingRepository) : IDashboardService
{
    private const int WeekDays = 7;
    private const int MonthDays = 30;

    public async Task<DashboardSummaryResponse> GetSummaryAsync(
        Guid codePartner,
        CancellationToken ct = default)
    {
        var now = DateTime.UtcNow;

        var counts = await onboardingRepository.GetDashboardCountsAsync(
                            codePartner,
                            now.AddDays(-WeekDays),
                            now.AddDays(-MonthDays),
                            ct);

        return new DashboardSummaryResponse(
                    counts.ReceivedInWeek,
                    counts.DeniedInWeek,
                    counts.ApprovedInWeek,
                    counts.PendingManualReview,
                    counts.ManuallyApprovedInMonth,
                    counts.ManuallyDeniedInMonth);
    }
}
