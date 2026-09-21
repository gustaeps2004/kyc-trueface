using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Exceptions;
using KYC.TrueFace.Core.Domain.Options;

namespace KYC.TrueFace.Core.Application.Messaging.DTOs;

public class OnboardingImageDto(
    string fileName,
    string contentType,
    long length,
    Stream content)
{
    public string FileName { get; } = fileName;
    public string ContentType { get; } = contentType;
    public long Length { get; } = length;
    public Stream Content { get; } = content;

    public void Validate(OnboardingOptions options)
    {
        if (Length <= 0)
            throw new KycException(ValidationErrors.OnboardingImageEmpty);

        if (Length > options.MaxImageSizeBytes)
            throw new KycException(ValidationErrors.OnboardingImageTooLarge);

        if (!options.AllowedContentTypes.Contains(ContentType, StringComparer.OrdinalIgnoreCase))
            throw new KycException(ValidationErrors.OnboardingImageContentTypeInvalid);
    }

    /// <summary>
    /// Extension taken from the uploaded name, restricted to the known image ones so a
    /// crafted file name can never drive the extension written to disk.
    /// </summary>
    public string GetSafeExtension()
    {
        var extension = Path.GetExtension(FileName);

        return OnboardingDefaults.AllowedImageExtensions.Contains(extension, StringComparer.OrdinalIgnoreCase)
            ? extension.ToLowerInvariant()
            : OnboardingDefaults.FallbackImageExtension;
    }
}
