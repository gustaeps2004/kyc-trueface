using KYC.TrueFace.Core.Domain.Entities;

namespace KYC.TrueFace.Core.Domain.Repositories;

public interface IUserReportRepository : IBaseRepository
{
    Task<IReadOnlyList<UserReport>> ClaimPendingAsync(
        int batchSize,
        DateTime staleBeforeUtc,
        CancellationToken ct = default);
}
