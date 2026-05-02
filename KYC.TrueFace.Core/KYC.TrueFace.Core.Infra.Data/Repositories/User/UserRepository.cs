using KYC.TrueFace.Core.Infra.Data.Data;
using KYC.TrueFace.Core.Infra.Data.Repositories.Base;

namespace KYC.TrueFace.Core.Infra.Data.Repositories.User;

public class UserRepository(
    ApplicationDbContext context) : BaseRepository(context), IUserRepository
{
    public bool IsExist(string idNumber, string email)
        => context
            .Users
            .Any(x => 
                    x.IdNumber.Equals(idNumber) ||
                    x.Email.Equals(email)
            );
}
