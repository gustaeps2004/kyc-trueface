using KYC.TrueFace.Core.Application.Messaging.DTOs;

namespace KYC.TrueFace.Core.Application.Services.Onboarding;

public interface IOnboardingImageStorage
{
    /// <summary>Persists the upload and returns its path relative to the storage root.</summary>
    Task<string> SaveAsync(
        Guid codePartner,
        Guid codeOnboarding,
        string kind,
        OnboardingImageDto image,
        CancellationToken ct = default);

    Task<byte[]> ReadAsync(string relativePath, CancellationToken ct = default);

    /// <summary>Best-effort removal used to roll back a partial upload; never throws.</summary>
    void Delete(string? relativePath);
}
