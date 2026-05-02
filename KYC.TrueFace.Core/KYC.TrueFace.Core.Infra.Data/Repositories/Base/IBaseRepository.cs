using Microsoft.EntityFrameworkCore.Storage;

namespace KYC.TrueFace.Core.Infra.Data.Repositories.Base;

public interface IBaseRepository
{
    void Insert<T>(T entity);
    void Update<T>(T entity);
    void SaveChanges();
    Task<IDbContextTransaction> BeginTransactionAsync();
}