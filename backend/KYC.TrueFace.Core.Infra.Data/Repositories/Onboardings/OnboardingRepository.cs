using KYC.TrueFace.Core.Domain.Entities;
using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.Repositories;
using KYC.TrueFace.Core.Domain.Repositories.Projections;
using KYC.TrueFace.Core.Infra.Data.Data;
using KYC.TrueFace.Core.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace KYC.TrueFace.Core.Infra.Data.Repositories.Onboardings;

public class OnboardingRepository(ApplicationDbContext context) : BaseRepository(context), IOnboardingRepository
{
    public async Task<IReadOnlyList<Onboarding>> ClaimPendingAsync(
        int batchSize,
        DateTime staleBeforeUtc,
        CancellationToken ct = default)
    {
        var claimed = await DbContext
                        .Onboardings
                        .Where(x =>
                            x.Situation == OnboardingSituation.Pending ||
                            (x.Situation == OnboardingSituation.Processing && x.SituationDt < staleBeforeUtc))
                        .OrderBy(x => x.InclusionDt)
                        .Take(batchSize)
                        .ToListAsync(ct);

        if (claimed.Count == 0)
            return [];

        foreach (var onboarding in claimed)
            onboarding.MarkAsProcessing();

        await DbContext.SaveChangesAsync(ct);

        return claimed;
    }

    public Task<Onboarding?> GetByCodeAsync(Guid code, CancellationToken ct = default)
        => DbContext
            .Onboardings
            .SingleOrDefaultAsync(x => x.Code.Equals(code), ct);

    public async Task<IReadOnlyList<Onboarding>> ListByPartnerAsync(
        Guid codePartner,
        OnboardingSituation[] situations,
        CancellationToken ct = default)
        => await DbContext
                .Onboardings
                .AsNoTracking()
                .Include(x => x.Results)
                .Where(x =>
                    x.CodePartner.Equals(codePartner) &&
                    situations.Contains(x.Situation))
                .OrderByDescending(x => x.SituationDt)
                .ToListAsync(ct);

    public async Task<OnboardingDashboardCounts> GetDashboardCountsAsync(
        Guid codePartner,
        DateTime weekStartUtc,
        DateTime monthStartUtc,
        CancellationToken ct = default)
    {
        // One aggregate per table, so the whole dashboard costs two round trips.
        var automatic = await DbContext
                            .Onboardings
                            .AsNoTracking()
                            .Where(x => x.CodePartner.Equals(codePartner))
                            .GroupBy(_ => 1)
                            .Select(g => new
                            {
                                ReceivedInWeek = g.Count(x => x.InclusionDt >= weekStartUtc),
                                DeniedInWeek = g.Count(x =>
                                    x.Situation == OnboardingSituation.Denied &&
                                    x.SituationDt >= weekStartUtc),
                                ApprovedInWeek = g.Count(x =>
                                    x.Situation == OnboardingSituation.Approved &&
                                    x.SituationDt >= weekStartUtc),
                                PendingManualReview = g.Count(x =>
                                    x.Situation == OnboardingSituation.ManualReview)
                            })
                            .SingleOrDefaultAsync(ct);

        // A result row exists only when a human settled the record, which is what tells
        // a manual decision apart from an automatic one. The join is spelled out because
        // counting over the navigation makes EF repeat it as a correlated subquery.
        var manual = await (from result in DbContext.OnboardingsResults.AsNoTracking()
                            join onboarding in DbContext.Onboardings
                                on result.CodeOnboarding equals onboarding.Code
                            where onboarding.CodePartner.Equals(codePartner) &&
                                  result.InclusionDt >= monthStartUtc
                            select onboarding.Situation)
                            .GroupBy(_ => 1)
                            .Select(g => new
                            {
                                ApprovedInMonth = g.Count(situation =>
                                    situation == OnboardingSituation.Approved),
                                DeniedInMonth = g.Count(situation =>
                                    situation == OnboardingSituation.Denied)
                            })
                            .SingleOrDefaultAsync(ct);

        if (automatic is null && manual is null)
            return OnboardingDashboardCounts.Empty;

        return new OnboardingDashboardCounts(
                    automatic?.ReceivedInWeek ?? 0,
                    automatic?.DeniedInWeek ?? 0,
                    automatic?.ApprovedInWeek ?? 0,
                    automatic?.PendingManualReview ?? 0,
                    manual?.ApprovedInMonth ?? 0,
                    manual?.DeniedInMonth ?? 0);
    }
}
