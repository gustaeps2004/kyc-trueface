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

    public bool IsPdf => ContentType.Equals(OnboardingDefaults.PdfContentType, StringComparison.OrdinalIgnoreCase);

    /// <param name="allowPdf">Only the document may be a PDF; the selfie must be an image.</param>
    public void Validate(OnboardingOptions options, bool allowPdf = false)
    {
        if (Length <= 0)
            throw new KycException(ValidationErrors.OnboardingImageEmpty);

        if (Length > options.MaxImageSizeBytes)
            throw new KycException(ValidationErrors.OnboardingImageTooLarge);

        if (allowPdf && IsPdf)
            return;

        if (!options.AllowedContentTypes.Contains(ContentType, StringComparer.OrdinalIgnoreCase))
            throw new KycException(allowPdf
                ? ValidationErrors.OnboardingDocumentContentTypeInvalid
                : ValidationErrors.OnboardingImageContentTypeInvalid);
    }

    public string GetSafeExtension()
        => OnboardingDefaults.ExtensionFor(ContentType);
}
