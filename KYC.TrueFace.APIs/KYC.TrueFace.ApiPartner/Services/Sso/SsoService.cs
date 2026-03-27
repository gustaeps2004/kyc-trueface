using KYC.TrueFace.ApiPartner.Messaging.Dtos;

namespace KYC.TrueFace.ApiPartner.Services.Sso;

public class SsoService : ISsoService
{
    public void RegisterPassword(
        RegisterPasswordDto registerDto, 
        Guid userCode)
    {
        registerDto.Validate();
    }
}