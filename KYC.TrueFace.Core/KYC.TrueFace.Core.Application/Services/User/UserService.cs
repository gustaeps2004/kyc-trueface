using KYC.TrueFace.Core.Application.Helpers;
using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Application.Services.UserAccess;
using KYC.TrueFace.Core.Domain.Exceptions;
using KYC.TrueFace.Core.Domain.Extensions;
using KYC.TrueFace.Core.Infra.Data.Repositories.User;

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
            throw new KycException("User is already existed.");

        userDto.Validate();

        using var ts = userRepository.BeginTransaction();

        var user = Insert(userDto, codePartner);

        userAccessService.Create(
            new CreateUserAccessDto(
                $"{userDto.Email}_onb",
                PasswordHelper.GenerateStrongRandom(),
                [userDto.Permission.GetDescription()],
                GetClaims(
                    user.Code,
                    codePartner,
                    user.Name
                )
            )
        );

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
            { "user_code", code.ToString() },
            { "user_code_partner", codePartner.ToString() },
            { "user_name", name },
        };
    }
}