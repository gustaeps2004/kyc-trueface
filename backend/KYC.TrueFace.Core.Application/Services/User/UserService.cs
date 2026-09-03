using KYC.TrueFace.Core.Application.Helpers;
using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Application.Services.Email;
using KYC.TrueFace.Core.Application.Services.Email.Templates;
using KYC.TrueFace.Core.Application.Services.Token;
using KYC.TrueFace.Core.Application.Services.UserAccess;
using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Exceptions;
using KYC.TrueFace.Core.Domain.Extensions;
using KYC.TrueFace.Core.Domain.Options;
using KYC.TrueFace.Core.Domain.Repositories;
using Microsoft.Extensions.Options;

namespace KYC.TrueFace.Core.Application.Services.User;

public class UserService(
    IUserRepository userRepository,
    IUserAccessService userAccessService,
    ITokenService tokenService,
    IEmailService emailService,
    IOptions<AppOptions> appOptions) : IUserService
{
    public async Task CreateAsync(
        CreateUserDto userDto,
        Guid codePartner,
        CancellationToken ct = default)
    {
        if (await userRepository.IsExistAsync(userDto.IdNumber, userDto.Email, ct))
            throw new KycException(ValidationErrors.UserExisted);

        userDto.Validate();

        var username = PasswordHelper.GetSuffix(userDto.Email);

        await using var ts = await userRepository.BeginTransactionAsync(ct);

        var user = Insert(userDto, codePartner);

        await userAccessService.CreateAsync(
            new CreateUserAccessDto(
                username,
                PasswordHelper.GenerateStrongRandom(),
                [userDto.Permission.GetDescription().ToUpper()],
                GetClaims(
                    user.Code,
                    codePartner,
                    user.Name
                )
            )
        );

        await userRepository.SaveChangesAsync(ct);
        await ts.CommitAsync(ct);

        await SendFirstAccessEmailAsync(userDto.Name, userDto.Email, username, ct);
    }

    private async Task SendFirstAccessEmailAsync(
        string name,
        string email,
        string username,
        CancellationToken ct)
    {
        var token = tokenService.GenerateToken(username, Roles.ResetPassword, additionalClaims: null);

        var link = $"{appOptions.Value.FrontendUrl.TrimEnd('/')}/register-password" +
                   $"?e={Uri.EscapeDataString(email)}&token={Uri.EscapeDataString(token)}";

        await emailService.SendAsync(
            new SendEmailDto(
                email,
                AccessEmailTemplate.FirstAccessSubject,
                AccessEmailTemplate.FirstAccess(name, link)
            ),
            ct
        );
    }

    private Domain.Entities.User Insert(
        CreateUserDto userDto,
        Guid codePartner)
    {
        var user = new Domain.Entities.User(
                        codePartner,
                        userDto.Name,
                        userDto.IdNumber,
                        userDto.BirthDate,
                        userDto.MotherName,
                        userDto.Email,
                        userDto.Permission
                    );

        userRepository.Insert(user);

        return user;
    }

    private Dictionary<string, string> GetClaims(
        Guid code,
        Guid codePartner,
        string name)
    {
        return new Dictionary<string, string>
        {
            { IdentityClaims.UserCode, code.ToString() },
            { IdentityClaims.PartnerCode, codePartner.ToString() },
            { IdentityClaims.UserName, name },
        };
    }

    public async Task<IEnumerable<UserResponse>> ListByPartnerAsync(
        Guid codePartner,
        string filter,
        CancellationToken ct = default)
    {
        var users = await userRepository.ListByPartnerAsync(codePartner, ct);

        var response = users
                        .Select(u => new UserResponse
                        {
                            Code = u.Code,
                            InclusionDt = u.InclusionDt,
                            Name = u.Name,
                            IdNumber = u.IdNumber,
                            BirthDate = u.BirthDate,
                            MotherName = u.MotherName,
                            Email = u.Email,
                            Permission = u.Permission,
                            Situation = u.Situation
                        });

        if (!string.IsNullOrEmpty(filter))
            response = response.Where(u =>
                            u.Name.Contains(filter, StringComparison.InvariantCultureIgnoreCase) ||
                            u.IdNumber.Contains(filter.JustNumbers()) ||
                            u.Email.Contains(filter, StringComparison.InvariantCultureIgnoreCase)
                        );

        return response;
    }

    public async Task UpdateAsync(
        UpdateUserDto userDto,
        Guid code,
        Guid codePartner,
        CancellationToken ct = default)
    {
        var user = await userRepository.GetByCodeAsync(code, ct)
                        ?? throw new KycException(ValidationErrors.UserNotExisted);

        // Treat a user from another partner as not found, so callers can't probe for existence across partners.
        if (user.CodePartner != codePartner)
            throw new KycException(ValidationErrors.UserNotExisted);

        userDto.Validate();

        user.Update(
            userDto.Name,
            userDto.BirthDate,
            userDto.MotherName,
            userDto.Situation,
            userDto.Permission);

        userRepository.Update(user);
        await userRepository.SaveChangesAsync(ct);
    }
}