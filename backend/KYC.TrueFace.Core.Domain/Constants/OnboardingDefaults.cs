namespace KYC.TrueFace.Core.Domain.Constants;

public static class OnboardingDefaults
{
    public const string DocumentKind = "document";

    public const string SelfieKind = "selfie";

    public const string FallbackImageExtension = ".jpg";

    public const string FallbackImageContentType = "image/jpeg";

    public const int ObservationMaxLength = 500;

    public static readonly string[] AllowedImageExtensions = [".jpg", ".jpeg", ".png"];

    public static string ContentTypeFor(string path)
        => Path.GetExtension(path).Equals(".png", StringComparison.OrdinalIgnoreCase)
            ? "image/png"
            : FallbackImageContentType;
}
