namespace KYC.TrueFace.Core.Domain.Repositories;

public interface IBaseRepository
{
    void Insert<T>(T entity) where T : class;
    void Update<T>(T entity) where T : class;
    void SaveChanges();
    ITransaction BeginTransaction();
}
