using KYC.TrueFace.Core.Application.Services.Onboarding;
using KYC.TrueFace.Core.Domain.Options;
using Microsoft.Extensions.Options;

namespace KYC.TrueFace.Core.API.BackgroundServices;

public class OnboardingWorker(
    IServiceScopeFactory scopeFactory,
    IOptions<OnboardingOptions> onboardingOptions,
    ILogger<OnboardingWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var interval = TimeSpan.FromSeconds(Math.Max(1, onboardingOptions.Value.PollingIntervalSeconds));
        using var timer = new PeriodicTimer(interval);

        logger.LogInformation("Onboarding worker started (interval {Interval}).", interval);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = scopeFactory.CreateScope();

                var processor = scope.ServiceProvider.GetRequiredService<IOnboardingProcessorService>();

                await processor.ProcessPendingAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled failure in the onboarding worker loop.");
            }

            try
            {
                if (!await timer.WaitForNextTickAsync(stoppingToken))
                    break;
            }
            catch (OperationCanceledException)
            {
                break;
            }
        }

        logger.LogInformation("Onboarding worker stopped.");
    }
}
