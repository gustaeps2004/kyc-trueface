using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Application.Messaging.DTOs;

public sealed record FaceComparisonResultDto(
    FaceComparisonOutcome Outcome,
    double? Similarity,
    string Message);
