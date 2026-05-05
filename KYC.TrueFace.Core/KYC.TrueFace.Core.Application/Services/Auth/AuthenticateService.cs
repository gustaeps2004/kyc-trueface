using KYC.TrueFace.Core.Application.Helpers;
using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Application.Services.Token;
using KYC.TrueFace.Core.Domain.Entities;
using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.Exceptions;
using KYC.TrueFace.Core.Infra.Data.Repositories.Base;
using KYC.TrueFace.Core.Infra.Data.Repositories.UsersAccess;
using Microsoft.AspNet.Identity;

namespace KYC.TrueFace.Core.Application.Services.Auth;

public class AuthenticateService(
    ITokenService tokenService,
    IBaseRepository baseRepository,
    IUserAccessRepository userAccessRepository) : IAuthenticateService
{
    private const string genericErrorMessage = "Incorect user or password.";

    public AuthenticateLoginResponse Login(
        LoginDto loginDto,
        string ip)
    {
        loginDto.Validate();

        var userAccess = userAccessRepository.GetByUsername(PasswordHelper.GetSufixx(loginDto.Email))
                            ?? throw new KycException(genericErrorMessage);

        var resultPassword = PasswordHelper.VerifyPassword(loginDto.Password, userAccess.Password);

        InsertLog(
            userAccess.Code,
            ip
        );

        if (resultPassword == PasswordVerificationResult.Failed)
            throw new KycException(genericErrorMessage);

        var token = tokenService.GenerateToken(
                        userAccess.Username,
                        userAccess.Role,
                        userAccess.Claim
                    );

        return new AuthenticateLoginResponse(token);
    }

    private void InsertLog(
        Guid codeUserAccess,
        string ip)
    {
        var log = new UserAccessLog(
                    codeUserAccess,
                    FlowIdentity.Loggin,
                    ip
                );

        baseRepository.Insert(log);
        baseRepository.SaveChanges();
    }
}