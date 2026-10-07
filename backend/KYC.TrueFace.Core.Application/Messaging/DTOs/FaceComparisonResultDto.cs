using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.ValueObjects;

namespace KYC.TrueFace.Core.Application.Messaging.DTOs;

public sealed record FaceComparisonResultDto(
    FaceComparisonOutcome Outcome,
    double? Similarity,
    LocalizedMessage Message);
