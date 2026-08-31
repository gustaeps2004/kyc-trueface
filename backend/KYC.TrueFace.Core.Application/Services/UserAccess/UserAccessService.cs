using KYC.TrueFace.Core.Application.Helpers;
using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Domain.Repositories;
using System.Text.Json;

namespace KYC.TrueFace.Core.Application.Services.UserAccess;

public class UserAccessService(
    IBaseRepository baseRepository) : IUserAccessService
{
    public async Task CreateAsync(CreateUserAccessDto userAccessDto)
    {
        var hashedPassword = await PasswordHelper.HashPasswordAsync(userAccessDto.Password);

        var userAccess = new Domain.Entities.UserAccess(
                            userAccessDto.Username,
                            hashedPassword,
                            string.Join(',', userAccessDto.Role),
                            JsonSerializer.Serialize(userAccessDto.Claims)
                        );

        baseRepository.Insert(userAccess);
    }
}