using KYC.TrueFace.Core.Domain.Entities;
using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.Repositories.Projections;

namespace KYC.TrueFace.Core.Domain.Repositories;

public interface IOnboardingRepository : IBaseRepository
{
    /// <summary>
    /// Atomically flips the next pending (or stale processing) records to
    /// <see cref="OnboardingSituation.Processing"/> and returns them.
    /// </summary>
    Task<IReadOnlyList<Onboarding>> ClaimPendingAsync(
        int batchSize,
        DateTime staleBeforeUtc,
        CancellationToken ct = default);

    Task<Onboarding?> GetByCodeAsync(Guid code, CancellationToken ct = default);

    /// <summary>Lists the partner's records in the given situations, with their manual review results.</summary>
    Task<IReadOnlyList<Onboarding>> ListByPartnerAsync(
        Guid codePartner,
        OnboardingSituation[] situations,
        CancellationToken ct = default);

    /// <summary>
    /// Aggregates the partner's dashboard counters. <paramref name="weekStartUtc"/> bounds the
    /// automatic figures and <paramref name="monthStartUtc"/> the manually reviewed ones.
    /// </summary>
    Task<OnboardingDashboardCounts> GetDashboardCountsAsync(
        Guid codePartner,
        DateTime weekStartUtc,
        DateTime monthStartUtc,
        CancellationToken ct = default);
}
