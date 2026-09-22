using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Application.Services.FaceComparison;
using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.Exceptions;
using KYC.TrueFace.Core.Domain.Options;
using KYC.TrueFace.Core.Domain.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace KYC.TrueFace.Core.Application.Services.Onboarding;

public class OnboardingProcessorService(
    IOnboardingRepository onboardingRepository,
    IOnboardingImageStorage imageStorage,
    IFaceComparisonService faceComparisonService,
    IOptions<OnboardingOptions> onboardingOptions,
    ILogger<OnboardingProcessorService> logger) : IOnboardingProcessorService
{
    public async Task ProcessPendingAsync(CancellationToken ct = default)
    {
        var options = onboardingOptions.Value;
        var staleBefore = DateTime.UtcNow.AddMinutes(-options.StaleProcessingMinutes);

        var claimed = await onboardingRepository.ClaimPendingAsync(options.BatchSize, staleBefore, ct);

        foreach (var onboarding in claimed)
        {
            if (ct.IsCancellationRequested)
                break;

            try
            {
                Apply(onboarding, await CompareAsync(onboarding.PathDocument, onboarding.PathSelfie, ct));
            }
            catch (OperationCanceledException) when (ct.IsCancellationRequested)
            {
                break;
            }
            catch (KycException ex)
            {
                // A missing or unreachable image will not fix itself on the next tick.
                onboarding.MarkForManualReview(ex.Message);
            }
            catch (Exception ex)
            {
                logger.LogWarning(ex, "Face comparison failed for onboarding {Code}.", onboarding.Code);

                var reason = $"{ex.GetType().Name}: {ex.Message}";

                if (onboarding.AttemptCount >= options.MaxAttempts)
                    onboarding.MarkForManualReview(
                        $"Max attempts ({options.MaxAttempts}) exceeded. Last failure - {reason}");
                else
                    onboarding.MarkForRetry(reason);
            }

            onboardingRepository.Update(onboarding);

            await onboardingRepository.SaveChangesAsync(CancellationToken.None);
        }
    }

    private async Task<FaceComparisonResultDto> CompareAsync(
        string pathDocument,
        string pathSelfie,
        CancellationToken ct)
    {
        var document = await imageStorage.ReadAsync(pathDocument, ct);
        var selfie = await imageStorage.ReadAsync(pathSelfie, ct);

        return await faceComparisonService.CompareAsync(document, selfie, ct);
    }

    private static void Apply(Domain.Entities.Onboarding onboarding, FaceComparisonResultDto result)
    {
        switch (result.Outcome)
        {
            case FaceComparisonOutcome.Matched:
                onboarding.MarkAsApproved(result.Similarity, result.Message);
                break;

            case FaceComparisonOutcome.NotMatched:
                onboarding.MarkAsDenied(result.Similarity, result.Message);
                break;

            case FaceComparisonOutcome.ReviewRequired:
                onboarding.MarkForManualReview(result.Message, result.Similarity);
                break;

            default:
                onboarding.MarkForManualReview(result.Message);
                break;
        }
    }
}
