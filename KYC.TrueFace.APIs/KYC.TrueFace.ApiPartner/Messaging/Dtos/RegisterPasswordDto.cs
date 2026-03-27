using KYC.TrueFace.ApiPartner.Exceptions;
using KYC.TrueFace.ApiPartner.Helpers;

namespace KYC.TrueFace.ApiPartner.Messaging.Dtos;

public class RegisterPasswordDto(
    string token,
    string email,
    string password,
    string confirmPassword)
{
    public string Token { get; set; } = token;
    public string Email { get; set; } = email;
    public string Password { get; set; } = password;
    public string ConfirmPassword { get; set; } = confirmPassword;

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Token))
            throw new TrueFaceException("Token must be provided.");

        if (string.IsNullOrWhiteSpace(Email))
            throw new TrueFaceException("E-mail must be provided.");

        if (string.IsNullOrWhiteSpace(Password))
            throw new TrueFaceException("Password must be provided.");

        if (string.IsNullOrWhiteSpace(ConfirmPassword))
            throw new TrueFaceException("Confirm password must be provided.");

        if (!Password.Equals(ConfirmPassword))
            throw new TrueFaceException("Password must be equals than confirm password.");

        if (!PasswordHelper.IsStrong(Password))
            throw new TrueFaceException("Password is weak.");
    }
}