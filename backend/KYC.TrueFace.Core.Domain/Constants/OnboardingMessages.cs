namespace KYC.TrueFace.Core.Domain.Constants;

/// <summary>
/// Translation keys recorded as the onboarding situation message. The values interpolated
/// into them (similarity, thresholds, attempts) are stored alongside as arguments.
/// </summary>
public static class OnboardingMessages
{
    public const string FacesMatched = "onboarding.result.facesMatched";
    public const string ManualReviewRequired = "onboarding.result.manualReviewRequired";
    public const string FacesNotMatched = "onboarding.result.facesNotMatched";
    public const string NoFaceFound = "onboarding.result.noFaceFound";
    public const string ImagesUnreadable = "onboarding.result.imagesUnreadable";
    public const string ImageFormatUnsupported = "onboarding.result.imageFormatUnsupported";
    public const string ImageTooLarge = "onboarding.result.imageTooLarge";
    public const string ProcessingFailed = "onboarding.result.processingFailed";
    public const string MaxAttemptsExceeded = "onboarding.result.maxAttemptsExceeded";
}
