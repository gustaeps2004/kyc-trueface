using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.Exceptions;
using KYC.TrueFace.Core.Domain.Options;
using KYC.TrueFace.Core.Domain.Repositories;
using Microsoft.Extensions.Options;

namespace KYC.TrueFace.Core.Application.Services.Onboarding;

public class OnboardingService(
    IOnboardingRepository onboardingRepository,
    IOnboardingImageStorage imageStorage,
    IPdfRenderer pdfRenderer,
    IOptions<OnboardingOptions> onboardingOptions) : IOnboardingService
{
    private static readonly OnboardingSituation[] ManualReviewSituations =
        [OnboardingSituation.ManualReview];

    private static readonly OnboardingSituation[] ReviewedSituations =
        [OnboardingSituation.Approved, OnboardingSituation.Denied];

    public async Task<OnboardingResponse> RegisterAsync(
        CreateOnboardingDto onboardingDto,
        Guid codePartner,
        CancellationToken ct = default)
    {
        onboardingDto.Validate(onboardingOptions.Value);

        var code = Guid.NewGuid();
        string? pathDocument = null;
        string? pathSelfie = null;

        try
        {
            pathDocument = await imageStorage.SaveAsync(
                                codePartner,
                                code,
                                OnboardingDefaults.DocumentKind,
                                onboardingDto.Document,
                                ct);

            // The PDF is only rendered later by the worker, so reject an unreadable one now
            // instead of leaving a record that can only end in manual review.
            if (onboardingDto.Document.IsPdf)
                pdfRenderer.EnsureReadable(await imageStorage.ReadAsync(pathDocument, ct));

            pathSelfie = await imageStorage.SaveAsync(
                                codePartner,
                                code,
                                OnboardingDefaults.SelfieKind,
                                onboardingDto.Selfie,
                                ct);

            var onboarding = new Domain.Entities.Onboarding(
                                code,
                                codePartner,
                                onboardingDto.IdNumber,
                                onboardingDto.Name,
                                pathDocument,
                                pathSelfie);

            onboardingRepository.Insert(onboarding);
            await onboardingRepository.SaveChangesAsync(ct);

            return new OnboardingResponse(onboarding.Code, onboarding.Situation);
        }
        catch
        {
            // Nothing references these files yet, so drop them instead of leaking disk space.
            imageStorage.Delete(pathDocument);
            imageStorage.Delete(pathSelfie);
            throw;
        }
    }

    public async Task<OnboardingImageFileResponse> GetImageAsync(
        Guid code,
        string kind,
        Guid codePartner,
        CancellationToken ct = default)
    {
        var onboarding = await GetForPartnerAsync(code, codePartner, ct);

        var relativePath = kind switch
        {
            OnboardingDefaults.DocumentKind => onboarding.PathDocument,
            OnboardingDefaults.SelfieKind => onboarding.PathSelfie,
            _ => throw new KycException(ValidationErrors.OnboardingImageKindInvalid)
        };

        var content = await imageStorage.ReadAsync(relativePath, ct);

        return new OnboardingImageFileResponse(
                    content,
                    OnboardingDefaults.ContentTypeFor(relativePath),
                    Path.GetFileName(relativePath));
    }

    public async Task ReviewAsync(
        ReviewOnboardingDto reviewDto,
        Guid code,
        Guid codeUser,
        Guid codePartner,
        CancellationToken ct = default)
    {
        reviewDto.Validate();

        var onboarding = await GetForPartnerAsync(code, codePartner, ct);

        if (onboarding.Situation != OnboardingSituation.ManualReview)
            throw new KycException(ValidationErrors.OnboardingNotPendingReview);

        onboarding.MarkAsManuallyReviewed(reviewDto.Approved, reviewDto.Observation);

        onboardingRepository.Update(onboarding);
        onboardingRepository.Insert(
            new Domain.Entities.OnboardingResult(onboarding.Code, codeUser, reviewDto.Observation));

        // Both writes land in the same SaveChanges, so they commit or roll back together.
        await onboardingRepository.SaveChangesAsync(ct);
    }

    public Task<IEnumerable<OnboardingListItemResponse>> ListPendingManualReviewAsync(
        Guid codePartner,
        CancellationToken ct = default)
        => ListAsync(codePartner, ManualReviewSituations, ct);

    public Task<IEnumerable<OnboardingListItemResponse>> ListReviewedAsync(
        Guid codePartner,
        OnboardingSituation? situation,
        CancellationToken ct = default)
    {
        if (situation is null)
            return ListAsync(codePartner, ReviewedSituations, ct);

        if (!ReviewedSituations.Contains(situation.Value))
            throw new KycException(ValidationErrors.OnboardingSituationInvalid);

        return ListAsync(codePartner, [situation.Value], ct);
    }

    private async Task<Domain.Entities.Onboarding> GetForPartnerAsync(
        Guid code,
        Guid codePartner,
        CancellationToken ct)
    {
        var onboarding = await onboardingRepository.GetByCodeAsync(code, ct)
                            ?? throw new KycException(ValidationErrors.OnboardingNotExisted);

        // Treat another partner's record as not found, so callers can't probe across partners.
        if (onboarding.CodePartner != codePartner)
            throw new KycException(ValidationErrors.OnboardingNotExisted);

        return onboarding;
    }

    private async Task<IEnumerable<OnboardingListItemResponse>> ListAsync(
        Guid codePartner,
        OnboardingSituation[] situations,
        CancellationToken ct)
    {
        var onboardings = await onboardingRepository.ListByPartnerAsync(codePartner, situations, ct);

        return onboardings
                .Select(o => new OnboardingListItemResponse
                {
                    Code = o.Code,
                    IdNumber = o.IdNumber,
                    Name = o.Name,
                    InclusionDt = o.InclusionDt,
                    Situation = o.Situation,
                    SituationDt = o.SituationDt,
                    SituationMessage = o.SituationMessage,
                    Similarity = o.Similarity,
                    AttemptCount = o.AttemptCount,
                    Observation = o.Results?
                                    .OrderByDescending(r => r.InclusionDt)
                                    .FirstOrDefault()?
                                    .Observation,
                    PathDocument = o.PathDocument,
                    PathSelfie = o.PathSelfie
                })
                .ToList();
    }
}
