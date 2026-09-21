using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Exceptions;
using KYC.TrueFace.Core.Domain.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KYC.TrueFace.Core.Application.Services.Onboarding;

/// <summary>
/// Stores the onboarding images on the VPS file system, under the folder configured in
/// <c>Onboarding:StoragePath</c>. Only the path relative to that root is persisted, so the
/// root can be remounted elsewhere without touching the database.
/// </summary>
public class OnboardingImageStorage(
    IOptions<OnboardingOptions> onboardingOptions,
    ILogger<OnboardingImageStorage> logger) : IOnboardingImageStorage
{
    public async Task<string> SaveAsync(
        Guid codePartner,
        Guid codeOnboarding,
        string kind,
        OnboardingImageDto image,
        CancellationToken ct = default)
    {
        var root = GetRoot();
        var now = DateTime.UtcNow;

        var relativeFolder = Path.Combine(codePartner.ToString("N"), now.ToString("yyyy"), now.ToString("MM"));
        var fileName = $"{codeOnboarding:N}-{kind}{image.GetSafeExtension()}";
        var relativePath = Path.Combine(relativeFolder, fileName);

        Directory.CreateDirectory(Path.Combine(root, relativeFolder));

        await using (var target = new FileStream(
                        Path.Combine(root, relativePath),
                        FileMode.CreateNew,
                        FileAccess.Write,
                        FileShare.None))
        {
            await image.Content.CopyToAsync(target, ct);
        }

        // Always store with forward slashes so the value is portable between Windows and Linux.
        return relativePath.Replace(Path.DirectorySeparatorChar, '/');
    }

    public async Task<byte[]> ReadAsync(string relativePath, CancellationToken ct = default)
    {
        var fullPath = Resolve(GetRoot(), relativePath);

        if (!File.Exists(fullPath))
            throw new KycException(ValidationErrors.OnboardingImageNotFound);

        return await File.ReadAllBytesAsync(fullPath, ct);
    }

    public void Delete(string? relativePath)
    {
        if (string.IsNullOrWhiteSpace(relativePath))
            return;

        try
        {
            var fullPath = Resolve(GetRoot(), relativePath);

            if (File.Exists(fullPath))
                File.Delete(fullPath);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "Could not delete onboarding image {RelativePath}.", relativePath);
        }
    }

    private string GetRoot()
    {
        var storagePath = onboardingOptions.Value.StoragePath;

        if (string.IsNullOrWhiteSpace(storagePath))
            throw new KycException(ValidationErrors.OnboardingStorageNotConfigured);

        // Trailing separators are trimmed so Resolve can prefix-match the root reliably.
        var root = Path.TrimEndingDirectorySeparator(Path.GetFullPath(storagePath));

        Directory.CreateDirectory(root);

        return root;
    }

    /// <summary>Resolves a stored path against the root, refusing anything that escapes it.</summary>
    private static string Resolve(string root, string relativePath)
    {
        var fullPath = Path.GetFullPath(Path.Combine(root, relativePath));

        if (!fullPath.StartsWith(root + Path.DirectorySeparatorChar, StringComparison.Ordinal))
            throw new KycException(ValidationErrors.OnboardingImageNotFound);

        return fullPath;
    }
}
