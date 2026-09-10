namespace KYC.TrueFace.Core.Application.Services.Report;

public interface IUserReportProcessorService
{
    Task ProcessPendingAsync(CancellationToken ct = default);
}
