using KYC.TrueFace.Core.Domain.Exceptions;
using KYC.TrueFace.Core.Domain.Extensions;

namespace KYC.TrueFace.Core.Application.Messaging.DTOs;

public class ResetPasswordDto(
    string email, 
    string token, 
    string password, 
    string confirmPassword)
{
    public string Email { get; set; } = email;
    public string Token { get; set; } = token;
    public string Password { get; set; } = password;
    public string ConfirmPassword { get; set; } = confirmPassword;

    public void Validate()
    {
        if (!Password.Equals(ConfirmPassword))
            throw new KycException("Password should be equals than confirm password.");

        if (!ValidationsExtension.IsValidPassword(Password))
            throw new KycException("Password is too weak.");
    }
}