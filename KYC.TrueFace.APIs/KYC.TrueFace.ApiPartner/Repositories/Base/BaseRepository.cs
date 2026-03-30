using KYC.TrueFace.ApiPartner.Repositories.Context;

namespace KYC.TrueFace.ApiPartner.Repositories.Base;

public class BaseRepository(AppDbContext context) : IBaseRepository
{
    public async Task AddAsync<TEntity>(TEntity entity)
        => await context.AddAsync(entity!);

    public void Update<TEntity>(TEntity entity)
        => context.Update(entity!);
}