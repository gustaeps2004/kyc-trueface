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
}
