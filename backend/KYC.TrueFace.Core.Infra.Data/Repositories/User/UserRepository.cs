using KYC.TrueFace.Core.Domain.Repositories;
using KYC.TrueFace.Core.Infra.Data.Data;
using KYC.TrueFace.Core.Infra.Data.Repositories.Base;

namespace KYC.TrueFace.Core.Infra.Data.Repositories.User;

public class UserRepository(ApplicationDbContext context) : BaseRepository(context), IUserRepository
{
    public bool IsExist(string idNumber, string email)
        => DbContext
            .Users
            .Any(x =>
                    x.IdNumber.Equals(idNumber) ||
                    x.Email.Equals(email)
            );

    public IEnumerable<Domain.Entities.User> ListByPartner(Guid codePartner)
        => DbContext
            .Users
            .Where(x => x.CodePartner.Equals(codePartner));

    public Domain.Entities.User? GetByCode(Guid code)
        => DbContext
            .Users
            .SingleOrDefault(x => x.Code.Equals(code));
}
