using KYC.TrueFace.Core.Application.Services.Report;
using KYC.TrueFace.Core.Domain.Options;
using Microsoft.Extensions.Options;

namespace KYC.TrueFace.Core.API.BackgroundServices;

public class UserReportWorker(
    IServiceScopeFactory scopeFactory,
    IOptions<UserReportOptions> reportOptions,
    ILogger<UserReportWorker> logger) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var interval = TimeSpan.FromSeconds(Math.Max(5, reportOptions.Value.PollingIntervalSeconds));
        using var timer = new PeriodicTimer(interval);

        logger.LogInformation("User report worker started (interval {Interval}).", interval);

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = scopeFactory.CreateScope();

                var processor = scope.ServiceProvider.GetRequiredService<IUserReportProcessorService>();

                await processor.ProcessPendingAsync(stoppingToken);
            }
            catch (OperationCanceledException) when (stoppingToken.IsCancellationRequested)
            {
                break;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Unhandled failure in the user report worker loop.");
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

        logger.LogInformation("User report worker stopped.");
    }
}
