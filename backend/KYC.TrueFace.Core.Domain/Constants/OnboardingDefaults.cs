namespace KYC.TrueFace.Core.Domain.Constants;

public static class OnboardingDefaults
{
    public const string DocumentKind = "document";

    public const string SelfieKind = "selfie";

    public const string FallbackImageExtension = ".jpg";

    public const string FallbackImageContentType = "image/jpeg";

    public const string PngContentType = "image/png";

    /// <summary>Accepted for the document only (e.g. the CNH-e), kept as-is for auditing.</summary>
    public const string PdfContentType = "application/pdf";

    public const string PdfExtension = ".pdf";

    public const int ObservationMaxLength = 500;

    /// <summary>
    /// Extension written to disk, taken from the validated content type so a crafted file
    /// name can never drive it.
    /// </summary>
    public static string ExtensionFor(string contentType)
        => contentType.ToLowerInvariant() switch
        {
            PngContentType => ".png",
            PdfContentType => PdfExtension,
            _ => FallbackImageExtension
        };

    public static string ContentTypeFor(string path)
        => Path.GetExtension(path).ToLowerInvariant() switch
        {
            ".png" => PngContentType,
            PdfExtension => PdfContentType,
            _ => FallbackImageContentType
        };

    public static bool IsPdf(string path)
        => Path.GetExtension(path).Equals(PdfExtension, StringComparison.OrdinalIgnoreCase);
}
