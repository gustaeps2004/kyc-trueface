using KYC.TrueFace.Core.Application.Messaging.Response;

namespace KYC.TrueFace.Core.Application.Services.Dashboard;

public interface IDashboardService
{
    /// <summary>Counters for the dashboard cards of the given partner.</summary>
    Task<DashboardSummaryResponse> GetSummaryAsync(
        Guid codePartner,
        CancellationToken ct = default);
}
