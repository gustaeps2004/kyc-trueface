using KYC.TrueFace.Core.Domain.Entities;

namespace KYC.TrueFace.Core.Domain.Repositories;

public interface IUserRepository : IBaseRepository
{
    User? GetByCode(Guid code);
    bool IsExist(string idNumber, string email);
    IEnumerable<User> ListByPartner(Guid codePartner);
}
