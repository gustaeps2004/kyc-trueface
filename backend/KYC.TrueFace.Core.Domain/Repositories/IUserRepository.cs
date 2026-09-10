using KYC.TrueFace.Core.Domain.Entities;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Domain.Repositories;

public interface IUserRepository : IBaseRepository
{
    Task<User?> GetByCodeAsync(Guid code, CancellationToken ct = default);
    Task<bool> IsExistAsync(string idNumber, string email, CancellationToken ct = default);
    Task<IEnumerable<User>> ListByPartnerAsync(Guid codePartner, CancellationToken ct = default);
    Task<IReadOnlyList<User>> ListForReportAsync(
        Guid codePartner,
        Situation? situation,
        DateTime? startDtUtc,
        DateTime? endDtUtcExclusive,
        CancellationToken ct = default);
}
