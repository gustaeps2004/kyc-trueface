using KYC.TrueFace.Core.Application.Helpers;
using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Application.Services.Token;
using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Entities;
using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.Exceptions;
using KYC.TrueFace.Core.Domain.Options;
using KYC.TrueFace.Core.Domain.Repositories;
using Microsoft.Extensions.Options;

namespace KYC.TrueFace.Core.Application.Services.Auth;

public class AuthenticateService(
    ITokenService tokenService,
    IBaseRepository baseRepository,
    IOptions<SsoOptions> ssoOptions,
    IUserAccessRepository userAccessRepository) : IAuthenticateService
{
    public async Task<AuthenticateLoginResponse> LoginAsync(
        LoginDto loginDto,
        string ip,
        CancellationToken ct = default)
    {
        loginDto.Validate();

        var userAccess = await userAccessRepository.GetByUsernameAsync(PasswordHelper.GetSuffix(loginDto.Email), ct)
                            ?? throw new KycException(ValidationErrors.AuthIncorrectUserOrPassword);

        var isValid = await PasswordHelper.IsValidPasswordAsync(loginDto.Password, userAccess.Password);

        await InsertLogAsync(
            userAccess.Code,
            FlowIdentity.Login,
            ip,
            ct
        );

        if (!isValid)
            throw new KycException(ValidationErrors.AuthIncorrectUserOrPassword);

        var token = tokenService.GenerateToken(
                        userAccess.Username,
                        userAccess.Role,
                        userAccess.Claim
                    );

        return new AuthenticateLoginResponse(token);
    }

    private async Task InsertLogAsync(
        Guid codeUserAccess,
        FlowIdentity flow,
        string ip,
        CancellationToken ct)
    {
        var log = new UserAccessLog(
                    codeUserAccess,
                    flow,
                    ip
                );

        baseRepository.Insert(log);
        await baseRepository.SaveChangesAsync(ct);
    }

    public async Task ResetPasswordAsync(
        ResetPasswordDto passwordDto,
        string ip,
        CancellationToken ct = default)
    {
        passwordDto.Validate();

        var userAccess = await userAccessRepository.GetByUsernameAsync(PasswordHelper.GetSuffix(passwordDto.Email), ct)
                            ?? throw new KycException(ValidationErrors.AuthIncorrectUserOrPassword);

        if (!PasswordHelper.IsValidToken(passwordDto.Token, userAccess.ResetPasswordTokenHash, userAccess.ResetPasswordTokenExpiresAt))
            throw new KycException(ValidationErrors.AuthInvalidOrExpiredResetToken);

        var newPassword = await PasswordHelper.HashPasswordAsync(passwordDto.Password);
        userAccess.UpdatePassword(newPassword);
        userAccess.ClearResetPasswordToken();

        await using var ts = await baseRepository.BeginTransactionAsync(ct);

        userAccessRepository.Update(userAccess);

        await InsertLogAsync(
            userAccess.Code,
            FlowIdentity.ChangePassword,
            ip,
            ct
        );

        await ts.CommitAsync(ct);
    }

    public async Task ForgotPasswordAsync(ForgotPasswordDto forgotPasswordDto, CancellationToken ct = default)
    {
        forgotPasswordDto.Validate();

        var userAccess = await userAccessRepository.GetByUsernameAsync(PasswordHelper.GetSuffix(forgotPasswordDto.Email), ct);

        if (userAccess is null)
            return;

        var token = PasswordHelper.GenerateSecureToken();

        userAccess.SetResetPasswordToken(
            PasswordHelper.HashToken(token),
            DateTime.UtcNow.AddMinutes(ssoOptions.Value.ResetPasswordTokenExpiration));

        userAccessRepository.Update(userAccess);
        await userAccessRepository.SaveChangesAsync(ct);
    }
}