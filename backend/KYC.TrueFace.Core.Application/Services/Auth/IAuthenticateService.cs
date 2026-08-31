using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Application.Messaging.Response;

namespace KYC.TrueFace.Core.Application.Services.Auth;

public interface IAuthenticateService
{
    Task<AuthenticateLoginResponse> LoginAsync(
        LoginDto loginDto,
        string ip,
        CancellationToken ct = default
    );
    Task ResetPasswordAsync(
        ResetPasswordDto passwordDto,
        string ip,
        CancellationToken ct = default
    );
    Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto, CancellationToken ct = default);
}