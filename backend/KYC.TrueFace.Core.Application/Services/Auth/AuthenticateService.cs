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
    public AuthenticateLoginResponse Login(
        LoginDto loginDto,
        string ip)
    {
        loginDto.Validate();

        var userAccess = userAccessRepository.GetByUsername(PasswordHelper.GetSuffix(loginDto.Email))
                            ?? throw new KycException(ValidationErrors.AuthIncorrectUserOrPassword);

        var isValid = PasswordHelper.IsValidPassword(loginDto.Password, userAccess.Password);

        InsertLog(
            userAccess.Code,
            FlowIdentity.Login,
            ip
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

    private void InsertLog(
        Guid codeUserAccess,
        FlowIdentity flow,
        string ip)
    {
        var log = new UserAccessLog(
                    codeUserAccess,
                    flow,
                    ip
                );

        baseRepository.Insert(log);
        baseRepository.SaveChanges();
    }

    public void ResetPassword(
        ResetPasswordDto passwordDto,
        string ip)
    {
        passwordDto.Validate();

        var userAccess = userAccessRepository.GetByUsername(PasswordHelper.GetSuffix(passwordDto.Email))
                            ?? throw new KycException(ValidationErrors.AuthIncorrectUserOrPassword);

        if (!PasswordHelper.IsValidToken(passwordDto.Token, userAccess.ResetPasswordTokenHash, userAccess.ResetPasswordTokenExpiresAt))
            throw new KycException(ValidationErrors.AuthInvalidOrExpiredResetToken);

        var newPassword = PasswordHelper.HashPassword(passwordDto.Password);
        userAccess.UpdatePassword(newPassword);
        userAccess.ClearResetPasswordToken();

        using var ts = baseRepository.BeginTransaction();

        userAccessRepository.Update(userAccess);

        InsertLog(
            userAccess.Code,
            FlowIdentity.ChangePassword,
            ip
        );

        ts.Commit();
    }

    public void ForgotPassword(ForgotPasswordDto forgotPasswordDto)
    {
        forgotPasswordDto.Validate();

        var userAccess = userAccessRepository.GetByUsername(PasswordHelper.GetSuffix(forgotPasswordDto.Email));

        if (userAccess is null)
            return;

        var token = PasswordHelper.GenerateSecureToken();

        userAccess.SetResetPasswordToken(
            PasswordHelper.HashToken(token),
            DateTime.UtcNow.AddMinutes(ssoOptions.Value.ResetPasswordTokenExpiration));

        userAccessRepository.Update(userAccess);
        userAccessRepository.SaveChanges();
    }
}