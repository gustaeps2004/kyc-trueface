using KYC.TrueFace.Core.Application.Messaging.DTOs;

namespace KYC.TrueFace.Core.Application.Messaging.Request;

public record ForgotPasswordRequest(
    string Email
)
{
    public ForgotPasswordDto ToDto() => new(Email);
}
