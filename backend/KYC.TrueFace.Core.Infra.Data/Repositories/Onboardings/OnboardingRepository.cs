using KYC.TrueFace.Core.Domain.Entities;
using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.Repositories;
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
}
