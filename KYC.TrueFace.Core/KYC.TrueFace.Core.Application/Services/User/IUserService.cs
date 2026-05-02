using KYC.TrueFace.Core.Application.Messaging.DTOs;

namespace KYC.TrueFace.Core.Application.Services.User;

public interface IUserService
{
    void Create(CreateUserDto userDto);
}