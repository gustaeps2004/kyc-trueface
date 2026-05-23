using KYC.TrueFace.Core.Domain.Entities;

namespace KYC.TrueFace.Core.Domain.Repositories;

public interface IUserAccessRepository : IBaseRepository
{
    UserAccess? GetByUsername(string username);
}
