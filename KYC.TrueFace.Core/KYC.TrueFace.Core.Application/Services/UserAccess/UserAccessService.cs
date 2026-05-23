using KYC.TrueFace.Core.Application.Helpers;
using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Domain.Repositories;
using System.Text.Json;

namespace KYC.TrueFace.Core.Application.Services.UserAccess;

public class UserAccessService(
    IBaseRepository baseRepository) : IUserAccessService
{
    public void Create(CreateUserAccessDto userAccessDto)
    {
        var userAccess = new Domain.Entities.UserAccess(
                            userAccessDto.Username,
                            PasswordHelper.HashPassword(userAccessDto.Password),
                            string.Join(',', userAccessDto.Role),
                            JsonSerializer.Serialize(userAccessDto.Claims)
                        );

        baseRepository.Insert(userAccess);
    }
}