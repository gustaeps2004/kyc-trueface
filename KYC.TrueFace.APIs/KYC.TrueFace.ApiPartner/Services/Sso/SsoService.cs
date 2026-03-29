using KYC.TrueFace.ApiPartner.Entities;
using KYC.TrueFace.ApiPartner.Helpers;
using KYC.TrueFace.ApiPartner.Messaging.Dtos;
using KYC.TrueFace.ApiPartner.Repositories.UserAccess;

namespace KYC.TrueFace.ApiPartner.Services.Sso;

public class SsoService(
    IUserAccessRepository userAccessRepository) : ISsoService
{
    public async Task RegisterPasswordAsync(RegisterPasswordDto registerDto)
    {
        registerDto.Validate();
        var hash = PasswordHelper.Hash(registerDto.Password);

        var userAccess = new UserAccess(
            registerDto.Email,
            hash
        );

        await userAccessRepository.AddAsync(userAccess);
    }
}