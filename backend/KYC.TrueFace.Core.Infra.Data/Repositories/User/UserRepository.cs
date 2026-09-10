using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.Repositories;
using KYC.TrueFace.Core.Infra.Data.Data;
using KYC.TrueFace.Core.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace KYC.TrueFace.Core.Infra.Data.Repositories.User;

public class UserRepository(ApplicationDbContext context) : BaseRepository(context), IUserRepository
{
    public Task<bool> IsExistAsync(string idNumber, string email, CancellationToken ct = default)
        => DbContext
            .Users
            .AnyAsync(x =>
                    x.IdNumber.Equals(idNumber) ||
                    x.Email.Equals(email),
                ct);

    public async Task<IEnumerable<Domain.Entities.User>> ListByPartnerAsync(Guid codePartner, CancellationToken ct = default)
        => await DbContext
            .Users
            .Where(x => x.CodePartner.Equals(codePartner))
            .ToListAsync(ct);

    public Task<Domain.Entities.User?> GetByCodeAsync(Guid code, CancellationToken ct = default)
        => DbContext
            .Users
            .SingleOrDefaultAsync(x => x.Code.Equals(code), ct);

    public async Task<IReadOnlyList<Domain.Entities.User>> ListForReportAsync(
        Guid codePartner,
        Situation? situation,
        DateTime? startDtUtc,
        DateTime? endDtUtcExclusive,
        CancellationToken ct = default)
    {
        var query = DbContext
                        .Users
                        .AsNoTracking()
                        .Where(x => x.CodePartner.Equals(codePartner));

        if (situation is not null)
            query = query.Where(x => x.Situation == situation.Value);

        if (startDtUtc is not null)
            query = query.Where(x => x.InclusionDt >= startDtUtc.Value);

        if (endDtUtcExclusive is not null)
            query = query.Where(x => x.InclusionDt < endDtUtcExclusive.Value);

        return await query
                        .OrderBy(x => x.Name)
                        .ToListAsync(ct);
    }
}
