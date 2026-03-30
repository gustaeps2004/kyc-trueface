using KYC.TrueFace.ApiPartner.Repositories.Base;
using KYC.TrueFace.ApiPartner.Repositories.Context;

namespace KYC.TrueFace.ApiPartner.Repositories.UserAccess;

public class UserAccessRepository(AppDbContext context) 
    : BaseRepository(context), IUserAccessRepository
{
}