namespace KYC.TrueFace.ApiPartner.Repositories.Base;

public interface IBaseRepository<TEntity> where TEntity : class
{
    Task AddAsync(TEntity entity);
    void Update(TEntity entity);
}
