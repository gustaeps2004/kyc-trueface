using KYC.TrueFace.Core.Domain.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace KYC.TrueFace.Core.Infra.Data.Data;

internal sealed class DbTransactionAdapter(IDbContextTransaction transaction) : ITransaction
{
    public void Commit() => transaction.Commit();
    public void Dispose() => transaction.Dispose();
}
