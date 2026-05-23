namespace KYC.TrueFace.Core.Domain.Repositories;

public interface IUserRepository : IBaseRepository
{
    bool IsExist(string idNumber, string email);
}
