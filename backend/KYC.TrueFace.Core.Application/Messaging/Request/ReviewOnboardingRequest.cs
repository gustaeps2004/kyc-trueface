using KYC.TrueFace.Core.Application.Messaging.DTOs;

namespace KYC.TrueFace.Core.Application.Messaging.Request;

public record ReviewOnboardingRequest
{
    public bool Approved { get; set; }
    public string? Observation { get; set; }

    public ReviewOnboardingDto ToDto()
        => new(Approved, Observation?.Trim() ?? string.Empty);
}
