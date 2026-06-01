using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Application.Messaging.Response;

namespace KYC.TrueFace.Core.Application.Services.User;

public interface IUserService
{
    void Create(
        CreateUserDto userDto,
        Guid codePartner);

    IEnumerable<UserResponse> ListByPartner(
        Guid codePartner,
        string filter);

    void Update(
        UpdateUserDto userDto, 
        Guid code);
}