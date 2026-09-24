using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Application.Services.UserAccess;

public interface IUserAccessService
{
    Task CreateAsync(CreateUserAccessDto userAccessDto);
    Task UpdateSituationAsync(string username, Situation situation);
}