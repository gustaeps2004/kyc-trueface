using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Exceptions;

namespace KYC.TrueFace.Core.Application.Messaging.DTOs;

public class ForgotPasswordDto(string email)
{
    public string Email { get; set; } = email;

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Email))
            throw new KycException(ValidationErrors.UserEmailNullOrEmpty);
    }
}
