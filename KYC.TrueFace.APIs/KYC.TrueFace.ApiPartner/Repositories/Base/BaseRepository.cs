using KYC.TrueFace.ApiPartner.Repositories.Context;

namespace KYC.TrueFace.ApiPartner.Repositories.Base;

public class BaseRepository<TEntity>(AppDbContext context) 
    : IBaseRepository<TEntity> where TEntity : class
{
    public async Task AddAsync(TEntity entity)
        => await context.AddAsync(entity);

    public void Update(TEntity entity)
        => context.Update(entity);
}