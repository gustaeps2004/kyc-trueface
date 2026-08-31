namespace KYC.TrueFace.Core.Domain.Repositories;

public interface ITransaction : IAsyncDisposable
{
    Task CommitAsync(CancellationToken ct = default);
}
