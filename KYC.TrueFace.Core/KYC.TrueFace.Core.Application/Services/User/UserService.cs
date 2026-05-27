using KYC.TrueFace.Core.Application.Helpers;
using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Application.Services.UserAccess;
using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Exceptions;
using KYC.TrueFace.Core.Domain.Extensions;
using KYC.TrueFace.Core.Domain.Repositories;

namespace KYC.TrueFace.Core.Application.Services.User;

public class UserService(
    IUserRepository userRepository,
    IUserAccessService userAccessService) : IUserService
{
    public void Create(
        CreateUserDto userDto,
        Guid codePartner)
    {
        if (userRepository.IsExist(userDto.IdNumber, userDto.Email))
            throw new KycException(ValidationErrors.UserExisted);

        userDto.Validate();

        using var ts = userRepository.BeginTransaction();

        var user = Insert(userDto, codePartner);

        userAccessService.Create(
            new CreateUserAccessDto(
                PasswordHelper.GetSuffix(userDto.Email),
                PasswordHelper.GenerateStrongRandom(),
                [userDto.Permission.GetDescription().ToUpper()],
                GetClaims(
                    user.Code,
                    codePartner,
                    user.Name
                )
            )
        );
        
        //send email to first access

        userRepository.SaveChanges();
        ts.Commit();
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

    public IEnumerable<UserResponse> ListByPartner(
        Guid codePartner,
        string filter)
    {
        var response = userRepository
                        .ListByPartner(codePartner)
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
}