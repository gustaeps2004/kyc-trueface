using KYC.TrueFace.Core.Application.Helpers;
using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Application.Security;
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
    IOptions<LoginSecurityOptions> loginSecurityOptions,
    IPasswordHasher passwordHasher,
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

        if (userAccess.IsLockedOut(DateTime.UtcNow))
        {
            await InsertLogAsync(userAccess.Code, FlowIdentity.Login, ip, ct);
            throw new KycException(ValidationErrors.AuthAccountLocked);
        }

        var verification = await passwordHasher.VerifyAsync(loginDto.Password, userAccess.Password, ct);

        await InsertLogAsync(
            userAccess.Code,
            FlowIdentity.Login,
            ip,
            ct
        );

        var lockout = loginSecurityOptions.Value;

        if (verification == PasswordVerificationResult.Failed)
        {
            userAccess.RegisterFailedLogin(lockout.MaxFailedAttempts, TimeSpan.FromMinutes(lockout.LockoutMinutes));
            userAccessRepository.Update(userAccess);
            await userAccessRepository.SaveChangesAsync(ct);

            throw new KycException(ValidationErrors.AuthIncorrectUserOrPassword);
        }

        var rehashNeeded = verification == PasswordVerificationResult.SuccessRehashNeeded;

        if (rehashNeeded || userAccess.AccessFailedCount != 0 || userAccess.LockoutEndsAt is not null)
        {
            userAccess.RegisterSuccessfulLogin();

            if (rehashNeeded)
                userAccess.UpdatePassword(await passwordHasher.HashAsync(loginDto.Password, ct));

            userAccessRepository.Update(userAccess);
            await userAccessRepository.SaveChangesAsync(ct);
        }

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

        var newPassword = await passwordHasher.HashAsync(passwordDto.Password, ct);
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