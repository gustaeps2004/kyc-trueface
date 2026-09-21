namespace KYC.TrueFace.Core.Application.Messaging.Response;

public sealed record OnboardingImageFileResponse(
    byte[] Content,
    string ContentType,
    string FileName);
