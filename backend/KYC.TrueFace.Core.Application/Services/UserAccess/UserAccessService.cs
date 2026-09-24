using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Application.Security;
using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.Repositories;
using System.Text.Json;

namespace KYC.TrueFace.Core.Application.Services.UserAccess;

public class UserAccessService(
    IBaseRepository baseRepository,
    IPasswordHasher passwordHasher,
    IUserAccessRepository userAccessRepository) : IUserAccessService
{
    public async Task CreateAsync(CreateUserAccessDto userAccessDto)
    {
        var hashedPassword = await passwordHasher.HashAsync(userAccessDto.Password);

        var userAccess = new Domain.Entities.UserAccess(
                            userAccessDto.Username,
                            hashedPassword,
                            string.Join(',', userAccessDto.Role),
                            JsonSerializer.Serialize(userAccessDto.Claims)
                        );

        baseRepository.Insert(userAccess);
    }

    public async Task UpdateSituationAsync(string username, Situation situation)
    {
        var userAccess = await userAccessRepository.GetByUsernameAsync(username);

        if (userAccess!.Situation == situation)
            return;

        userAccess.UpdateSituation(situation);
        userAccessRepository.Update(userAccess);
    }
}
