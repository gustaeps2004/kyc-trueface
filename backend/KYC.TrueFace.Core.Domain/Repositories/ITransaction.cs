namespace KYC.TrueFace.Core.Domain.Repositories;

public interface ITransaction : IDisposable
{
    void Commit();
}
