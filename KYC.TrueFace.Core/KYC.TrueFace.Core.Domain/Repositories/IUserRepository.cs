using KYC.TrueFace.Core.Domain.Entities;

namespace KYC.TrueFace.Core.Domain.Repositories;

public interface IUserRepository : IBaseRepository
{
    bool IsExist(string idNumber, string email);
    IEnumerable<User> ListByPartner(Guid codePartner);
}
