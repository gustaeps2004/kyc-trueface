using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Exceptions;

namespace KYC.TrueFace.Core.Application.Messaging.DTOs;

public class ReviewOnboardingDto(bool approved, string observation)
{
    public bool Approved { get; } = approved;
    public string Observation { get; } = observation;

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Observation))
            throw new KycException(ValidationErrors.OnboardingObservationNullOrEmpty);

        if (Observation.Length > OnboardingDefaults.ObservationMaxLength)
            throw new KycException(ValidationErrors.OnboardingObservationExceed);
    }
}
