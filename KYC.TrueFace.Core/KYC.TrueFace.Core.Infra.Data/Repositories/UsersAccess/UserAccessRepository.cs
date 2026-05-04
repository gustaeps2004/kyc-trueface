using KYC.TrueFace.Core.Infra.Data.Data;
using KYC.TrueFace.Core.Infra.Data.Repositories.Base;

namespace KYC.TrueFace.Core.Infra.Data.Repositories.UsersAccess;

public class UserAccessRepository(
    ApplicationDbContext context) : BaseRepository(context), IUserAccessRepository
{
    public Domain.Entities.UserAccess? GetByUsername(string username)
        => context.UsersAccess.SingleOrDefault(u => u.Username == username);
}
