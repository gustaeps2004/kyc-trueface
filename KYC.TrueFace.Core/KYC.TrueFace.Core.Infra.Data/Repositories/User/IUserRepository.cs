using KYC.TrueFace.Core.Infra.Data.Repositories.Base;

namespace KYC.TrueFace.Core.Infra.Data.Repositories.User;

public interface IUserRepository : IBaseRepository
{
    bool IsExist(
        string idNumber,
        string email
    );
}