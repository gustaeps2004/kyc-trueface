using KYC.TrueFace.Core.Domain.Entities;
using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.Repositories;
using KYC.TrueFace.Core.Infra.Data.Data;
using KYC.TrueFace.Core.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace KYC.TrueFace.Core.Infra.Data.Repositories.UsersReports;

public class UserReportRepository(ApplicationDbContext context) : BaseRepository(context), IUserReportRepository
{
    public async Task<IReadOnlyList<UserReport>> ClaimPendingAsync(
        int batchSize,
        DateTime staleBeforeUtc,
        CancellationToken ct = default)
    {
        var claimed = await DbContext
                        .UsersReports
                        .Where(x =>
                            x.Situation == UserReportSituation.Pending ||
                            (x.Situation == UserReportSituation.Processing && x.SituationDt < staleBeforeUtc))
                        .OrderBy(x => x.InclusionDt)
                        .Take(batchSize)
                        .ToListAsync(ct);

        if (claimed.Count == 0)
            return [];

        foreach (var report in claimed)
            report.MarkAsProcessing();

        await DbContext.SaveChangesAsync(ct);

        return claimed;
    }
}
