using System.ComponentModel.DataAnnotations;
using KYC.TrueFace.Core.Application.Messaging.DTOs;

namespace KYC.TrueFace.Core.Application.Messaging.Response;

public record ResetPasswordRequest
{
    [MinLength(1, ErrorMessage = "E-mail should be informed.")]
    public required string Email { get; set; }

    [MinLength(1, ErrorMessage = "Password should be informed.")]
    public required string Password { get; set; }

    [MinLength(1, ErrorMessage = "Confirm password should be informed.")]
    public required string ConfirmPassword { get; set; }

    public ResetPasswordDto ToDto()
        => new(
                Email,
                Password,
                ConfirmPassword
            );
}
