using KYC.TrueFace.Core.Domain.Entities;

namespace KYC.TrueFace.Core.Domain.Repositories;

public interface IUserAccessRepository : IBaseRepository
{
    Task<UserAccess?> GetByUsernameAsync(string username, CancellationToken ct = default);
}
