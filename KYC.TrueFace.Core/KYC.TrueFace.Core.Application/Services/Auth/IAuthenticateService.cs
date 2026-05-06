using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Application.Messaging.Response;

namespace KYC.TrueFace.Core.Application.Services.Auth;

public interface IAuthenticateService
{
    AuthenticateLoginResponse Login(
        LoginDto loginDto,
        string ip
    );
    void ResetPassword(
        ResetPasswordDto passwordDto,
        string ip
    );
}