using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Application.Services.Onboarding;

public interface IOnboardingService
{
    /// <summary>Stores both images and queues the onboarding for automatic validation.</summary>
    Task<OnboardingResponse> RegisterAsync(
        CreateOnboardingDto onboardingDto,
        Guid codePartner,
        CancellationToken ct = default);

    /// <summary>Records the automatic validation could not decide on - waiting for a human.</summary>
    Task<IEnumerable<OnboardingListItemResponse>> ListPendingManualReviewAsync(
        Guid codePartner,
        CancellationToken ct = default);

    /// <summary>Reads back one of the stored images so a reviewer can look at it.</summary>
    Task<OnboardingImageFileResponse> GetImageAsync(
        Guid code,
        string kind,
        Guid codePartner,
        CancellationToken ct = default);

    /// <summary>Settles a record the automatic validation sent to manual review.</summary>
    Task ReviewAsync(
        ReviewOnboardingDto reviewDto,
        Guid code,
        Guid codeUser,
        Guid codePartner,
        CancellationToken ct = default);

    /// <summary>Records already settled as approved or denied.</summary>
    Task<IEnumerable<OnboardingListItemResponse>> ListReviewedAsync(
        Guid codePartner,
        OnboardingSituation? situation,
        CancellationToken ct = default);
}
