namespace KYC.TrueFace.ApiPartner.Repositories.Base;

public interface IBaseRepository
{
    Task AddAsync<TEntity>(TEntity entity);
    void Update<TEntity>(TEntity entity);
}
