using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Application.Messaging.Response;

namespace KYC.TrueFace.Core.Application.Services.User;

public interface IUserService
{
    Task CreateAsync(
        CreateUserDto userDto,
        Guid codePartner,
        CancellationToken ct = default);

    Task<IEnumerable<UserResponse>> ListByPartnerAsync(
        Guid codePartner,
        string filter,
        CancellationToken ct = default);

    Task UpdateAsync(
        UpdateUserDto userDto,
        Guid code,
        Guid codePartner,
        CancellationToken ct = default);
}