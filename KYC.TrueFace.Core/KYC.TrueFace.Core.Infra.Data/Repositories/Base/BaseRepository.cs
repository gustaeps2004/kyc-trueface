using KYC.TrueFace.Core.Domain.Repositories;
using KYC.TrueFace.Core.Infra.Data.Data;

namespace KYC.TrueFace.Core.Infra.Data.Repositories.Base;

public class BaseRepository(ApplicationDbContext context) : IBaseRepository
{
    protected readonly ApplicationDbContext DbContext = context;

    public void Insert<T>(T entity) where T : class
        => DbContext.Add(entity);

    public void Update<T>(T entity) where T : class
        => DbContext.Update(entity);

    public void SaveChanges()
        => DbContext.SaveChanges();

    public ITransaction BeginTransaction()
        => new DbTransactionAdapter(DbContext.Database.BeginTransaction());
}
