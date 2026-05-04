using KYC.TrueFace.Core.Application.Messaging.DTOs;

namespace KYC.TrueFace.Core.Application.Messaging.Request;

public record LoginRequest(
    string Email,
    string Password
)
{
    public LoginDto ToDto() => new(Email, Password);
}
