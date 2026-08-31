using KYC.TrueFace.Core.Domain.Repositories;
using KYC.TrueFace.Core.Infra.Data.Data;
using KYC.TrueFace.Core.Infra.Data.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace KYC.TrueFace.Core.Infra.Data.Repositories.UsersAccess;

public class UserAccessRepository(ApplicationDbContext context) : BaseRepository(context), IUserAccessRepository
{
    public Task<Domain.Entities.UserAccess?> GetByUsernameAsync(string username, CancellationToken ct = default)
        => DbContext.UsersAccess.SingleOrDefaultAsync(u => u.Username == username, ct);
}
