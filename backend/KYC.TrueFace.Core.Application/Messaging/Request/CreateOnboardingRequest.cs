using KYC.TrueFace.Core.Application.Messaging.DTOs;
using Microsoft.AspNetCore.Http;

namespace KYC.TrueFace.Core.Application.Messaging.Request;

public class CreateOnboardingRequest
{
    /// <summary>Document number of the person being onboarded.</summary>
    public string? IdNumber { get; set; }

    /// <summary>Name of the person being onboarded.</summary>
    public string? Name { get; set; }

    /// <summary>
    /// Identity document containing the reference face: a JPEG/PNG picture or a PDF such as
    /// the CNH-e. A PDF is stored as uploaded and its first page is used for the comparison.
    /// </summary>
    public IFormFile? Document { get; set; }

    /// <summary>Selfie taken by the person being onboarded.</summary>
    public IFormFile? Selfie { get; set; }

    public CreateOnboardingDto ToDto()
        => new(
                IdNumber?.Trim() ?? string.Empty,
                Name?.Trim() ?? string.Empty,
                ToImageDto(Document),
                ToImageDto(Selfie)
            );

    private static OnboardingImageDto? ToImageDto(IFormFile? file)
        => file is null
            ? null
            : new OnboardingImageDto(
                    file.FileName,
                    file.ContentType ?? string.Empty,
                    file.Length,
                    file.OpenReadStream()
                );
}
