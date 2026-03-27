using KYC.TrueFace.ApiPartner.Messaging.Dtos;

namespace KYC.TrueFace.ApiPartner.Messaging.Request;

public record RegisterPasswordRequest
{
    public required string Token { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string ConfirmPassword { get; set; }

    public RegisterPasswordDto ToDto()
        => new(Token, Email, Password, ConfirmPassword);
}