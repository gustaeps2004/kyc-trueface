namespace KYC.TrueFace.Core.Domain.Repositories;

public interface IBaseRepository
{
    void Insert<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    Task SaveChangesAsync(CancellationToken ct = default);
    Task<ITransaction> BeginTransactionAsync(CancellationToken ct = default);
}
