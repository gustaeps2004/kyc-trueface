namespace KYC.TrueFace.Core.Application.Services.Onboarding;

public interface IOnboardingProcessorService
{
    Task ProcessPendingAsync(CancellationToken ct = default);
}
