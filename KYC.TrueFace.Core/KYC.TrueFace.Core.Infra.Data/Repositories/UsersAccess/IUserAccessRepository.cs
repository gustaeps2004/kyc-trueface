using KYC.TrueFace.Core.Infra.Data.Repositories.Base;

namespace KYC.TrueFace.Core.Infra.Data.Repositories.UsersAccess;

public interface IUserAccessRepository : IBaseRepository
{
    Domain.Entities.UserAccess? GetByUsername(string username);
}