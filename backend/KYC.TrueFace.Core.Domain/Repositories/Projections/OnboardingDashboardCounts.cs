namespace KYC.TrueFace.Core.Domain.Repositories.Projections;

/// <summary>
/// Counts behind the dashboard cards. Every number is already scoped to one partner.
/// </summary>
public sealed record OnboardingDashboardCounts(
    int ReceivedInWeek,
    int DeniedInWeek,
    int ApprovedInWeek,
    int PendingManualReview,
    int ManuallyApprovedInMonth,
    int ManuallyDeniedInMonth)
{
    public static OnboardingDashboardCounts Empty { get; } = new(0, 0, 0, 0, 0, 0);
}
