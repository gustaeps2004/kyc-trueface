using KYC.TrueFace.Core.Application.Messaging.DTOs;

namespace KYC.TrueFace.Core.Application.Services.UserAccess;

public interface IUserAccessService
{
    void Create(CreateUserAccessDto userAccessDto);
}