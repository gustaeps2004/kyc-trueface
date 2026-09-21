using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Application.Messaging.Response;

public sealed record OnboardingResponse(Guid Code, OnboardingSituation Situation);
