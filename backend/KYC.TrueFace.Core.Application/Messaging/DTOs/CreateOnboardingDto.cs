using System.Diagnostics.CodeAnalysis;
using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Entities;
using KYC.TrueFace.Core.Domain.Exceptions;
using KYC.TrueFace.Core.Domain.Extensions;
using KYC.TrueFace.Core.Domain.Options;

namespace KYC.TrueFace.Core.Application.Messaging.DTOs;

public class CreateOnboardingDto(
    string idNumber,
    string name,
    OnboardingImageDto? document,
    OnboardingImageDto? selfie)
{
    public string IdNumber { get; } = idNumber;
    public string Name { get; } = name;
    public OnboardingImageDto? Document { get; } = document;
    public OnboardingImageDto? Selfie { get; } = selfie;

    [MemberNotNull(nameof(Document), nameof(Selfie))]
    public void Validate(OnboardingOptions options)
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new KycException(ValidationErrors.OnboardingNameNullOrEmpty);

        if (Name.Length > Onboarding.MaxNameLength)
            throw new KycException(ValidationErrors.OnboardingNameExceed);

        if (ValidationsExtension.IsIdNumberInvalid(IdNumber))
            throw new KycException(ValidationErrors.OnboardingInvalidIdNumber);

        if (Document is null)
            throw new KycException(ValidationErrors.OnboardingDocumentRequired);

        if (Selfie is null)
            throw new KycException(ValidationErrors.OnboardingSelfieRequired);

        Document.Validate(options, allowPdf: true);
        Selfie.Validate(options);
    }
}
