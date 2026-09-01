using KYC.TrueFace.Core.Application.Messaging.DTOs;

namespace KYC.TrueFace.Core.Application.Services.Email;

public interface IEmailService
{
    Task SendAsync(
        SendEmailDto emailDto,
        CancellationToken ct = default);
}
