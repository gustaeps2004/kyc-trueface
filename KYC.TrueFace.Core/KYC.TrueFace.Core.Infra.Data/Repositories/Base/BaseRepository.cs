using KYC.TrueFace.Core.Infra.Data.Data;
using Microsoft.EntityFrameworkCore.Storage;

namespace KYC.TrueFace.Core.Infra.Data.Repositories.Base;

public class BaseRepository(
    ApplicationDbContext context) : IBaseRepository
{
    public void Insert<T>(T entity)
        => context.Add(entity!);

    public void Update<T>(T entity)
        => context.Update(entity!);

    public void SaveChanges()
        => context.SaveChanges();

    public IDbContextTransaction BeginTransaction()
        => context.Database.BeginTransaction();
}