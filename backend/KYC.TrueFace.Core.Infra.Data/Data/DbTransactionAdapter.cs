using KYC.TrueFace.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace KYC.TrueFace.Core.Infra.Data.Data;

internal sealed class DbTransactionAdapter(IDbContextTransaction transaction) : ITransaction
{
    public Task CommitAsync(CancellationToken ct = default) => transaction.CommitAsync(ct);
    public ValueTask DisposeAsync() => transaction.DisposeAsync();
}
