using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Exceptions;

namespace KYC.TrueFace.Core.Application.Messaging.DTOs;

public class LoginDto(
    string email,
    string password)
{
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Email))
            throw new KycException(ValidationErrors.UserEmailNullOrEmpty);

        if (string.IsNullOrWhiteSpace(Password))
            throw new KycException(ValidationErrors.UserPasswordCannotBeNullOrEmpty);
    }
}