using KYC.TrueFace.ApiPartner.Messaging.Dtos;

namespace KYC.TrueFace.ApiPartner.Services.Sso;

public interface ISsoService
{
    Task RegisterPasswordAsync(RegisterPasswordDto registerDto);
}