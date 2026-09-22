namespace KYC.TrueFace.Core.Application.Messaging.Response;

/// <summary>Numbers shown on the dashboard cards, for the partner of the logged user.</summary>
/// <param name="ConsultedLastWeek">Onboardings received in the last week, whatever their situation.</param>
/// <param name="ReprovedLastWeek">Onboardings denied in the last week.</param>
/// <param name="ApprovedLastWeek">Onboardings approved in the last week.</param>
/// <param name="PendingManualReview">Records waiting for a human decision right now - not bound to a window.</param>
/// <param name="ApprovedManuallyLastMonth">Records a reviewer approved in the last month.</param>
/// <param name="ReprovedManuallyLastMonth">Records a reviewer denied in the last month.</param>
public sealed record DashboardSummaryResponse(
    int ConsultedLastWeek,
    int ReprovedLastWeek,
    int ApprovedLastWeek,
    int PendingManualReview,
    int ApprovedManuallyLastMonth,
    int ReprovedManuallyLastMonth);
