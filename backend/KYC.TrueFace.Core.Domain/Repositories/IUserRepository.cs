using KYC.TrueFace.Core.Domain.Entities;

namespace KYC.TrueFace.Core.Domain.Repositories;

public interface IUserRepository : IBaseRepository
{
    Task<User?> GetByCodeAsync(Guid code, CancellationToken ct = default);
    Task<bool> IsExistAsync(string idNumber, string email, CancellationToken ct = default);
    Task<IEnumerable<User>> ListByPartnerAsync(Guid codePartner, CancellationToken ct = default);
}
