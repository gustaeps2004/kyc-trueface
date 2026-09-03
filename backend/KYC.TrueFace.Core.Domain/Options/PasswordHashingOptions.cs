namespace KYC.TrueFace.Core.Domain.Options;

public class PasswordHashingOptions
{
    public const string SectionName = "PasswordHashing";

    public int MemorySizeKib { get; init; } = 19456;

    public int Iterations { get; init; } = 2;

    public int Parallelism { get; init; } = 1;

    public int SaltSize { get; init; } = 16;

    public int HashSize { get; init; } = 32;

    public int MaxConcurrentHashes { get; init; }

    public string Pepper { get; init; } = string.Empty;
}
