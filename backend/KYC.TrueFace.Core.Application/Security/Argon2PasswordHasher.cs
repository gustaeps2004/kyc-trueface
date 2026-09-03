using System.Security.Cryptography;
using System.Text;
using KYC.TrueFace.Core.Domain.Options;
using Konscious.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace KYC.TrueFace.Core.Application.Security;

public sealed class Argon2PasswordHasher : IPasswordHasher, IDisposable
{
    private const string Scheme = "argon2id";
    private const int Version = 19;

    private readonly PasswordHashingOptions _options;
    private readonly byte[]? _pepper;
    private readonly SemaphoreSlim _gate;

    public Argon2PasswordHasher(IOptions<PasswordHashingOptions> options)
    {
        _options = options.Value;
        _pepper = string.IsNullOrEmpty(_options.Pepper) ? null : Encoding.UTF8.GetBytes(_options.Pepper);

        var permits = _options.MaxConcurrentHashes > 0 ? _options.MaxConcurrentHashes : Environment.ProcessorCount;
        _gate = new SemaphoreSlim(permits, permits);
    }

    public string Hash(string password)
    {
        ArgumentException.ThrowIfNullOrEmpty(password);

        var salt = RandomNumberGenerator.GetBytes(_options.SaltSize);
        _gate.Wait();
        try
        {
            return DeriveAndFormat(password, salt);
        }
        finally
        {
            _gate.Release();
        }
    }

    public async Task<string> HashAsync(string password, CancellationToken ct = default)
    {
        ArgumentException.ThrowIfNullOrEmpty(password);

        var salt = RandomNumberGenerator.GetBytes(_options.SaltSize);
        await _gate.WaitAsync(ct).ConfigureAwait(false);
        try
        {
            return await Task.Run(() => DeriveAndFormat(password, salt), ct).ConfigureAwait(false);
        }
        finally
        {
            _gate.Release();
        }
    }

    public PasswordVerificationResult Verify(string password, string storedHash)
    {
        if (string.IsNullOrEmpty(password) || !TryParse(storedHash, out var p))
            return PasswordVerificationResult.Failed;

        if (p.Keyed && _pepper is null)
            return PasswordVerificationResult.Failed;

        _gate.Wait();
        try
        {
            return Compare(password, p);
        }
        finally
        {
            _gate.Release();
        }
    }

    public async Task<PasswordVerificationResult> VerifyAsync(string password, string storedHash, CancellationToken ct = default)
    {
        if (string.IsNullOrEmpty(password) || !TryParse(storedHash, out var p))
            return PasswordVerificationResult.Failed;

        if (p.Keyed && _pepper is null)
            return PasswordVerificationResult.Failed;

        await _gate.WaitAsync(ct).ConfigureAwait(false);
        try
        {
            return await Task.Run(() => Compare(password, p), ct).ConfigureAwait(false);
        }
        finally
        {
            _gate.Release();
        }
    }

    public void Dispose() => _gate.Dispose();

    private string DeriveAndFormat(string password, byte[] salt)
    {
        var keyed = _pepper is not null;
        var hash = Derive(password, salt, _options.MemorySizeKib, _options.Iterations, _options.Parallelism, _options.HashSize, keyed);

        return $"${Scheme}$v={Version}$m={_options.MemorySizeKib},t={_options.Iterations},p={_options.Parallelism},k={(keyed ? 1 : 0)}$" +
               $"{Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
    }

    private PasswordVerificationResult Compare(string password, ParsedHash p)
    {
        var computed = Derive(password, p.Salt, p.MemorySizeKib, p.Iterations, p.Parallelism, p.Hash.Length, p.Keyed);

        if (!CryptographicOperations.FixedTimeEquals(computed, p.Hash))
            return PasswordVerificationResult.Failed;

        var stale =
            p.MemorySizeKib != _options.MemorySizeKib ||
            p.Iterations != _options.Iterations ||
            p.Parallelism != _options.Parallelism ||
            p.Hash.Length != _options.HashSize ||
            p.Salt.Length != _options.SaltSize ||
            p.Keyed != (_pepper is not null);

        return stale ? PasswordVerificationResult.SuccessRehashNeeded : PasswordVerificationResult.Success;
    }

    private byte[] Derive(string password, byte[] salt, int memoryKib, int iterations, int parallelism, int hashSize, bool keyed)
    {
        using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
        {
            Salt = salt,
            MemorySize = memoryKib,
            Iterations = iterations,
            DegreeOfParallelism = parallelism
        };

        if (keyed)
            argon2.KnownSecret = _pepper;

        return argon2.GetBytes(hashSize);
    }

    private readonly record struct ParsedHash(
        byte[] Salt, byte[] Hash, int MemorySizeKib, int Iterations, int Parallelism, bool Keyed);

    private static bool TryParse(string? stored, out ParsedHash parsed)
    {
        parsed = default;
        if (string.IsNullOrWhiteSpace(stored))
            return false;

        var parts = stored.Split('$', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 5 || !parts[0].Equals(Scheme, StringComparison.Ordinal))
            return false;

        int memory = 0, iterations = 0, parallelism = 0, keyed = 0;
        foreach (var pair in parts[2].Split(',', StringSplitOptions.RemoveEmptyEntries))
        {
            var kv = pair.Split('=', 2);
            if (kv.Length != 2 || !int.TryParse(kv[1], out var value))
                return false;

            switch (kv[0])
            {
                case "m": memory = value; break;
                case "t": iterations = value; break;
                case "p": parallelism = value; break;
                case "k": keyed = value; break;
                default: return false;
            }
        }

        if (memory <= 0 || iterations <= 0 || parallelism <= 0 || keyed is < 0 or > 1)
            return false;

        if (!TryFromBase64(parts[3], out var salt) || !TryFromBase64(parts[4], out var hash) ||
            salt.Length == 0 || hash.Length == 0)
            return false;

        parsed = new ParsedHash(salt, hash, memory, iterations, parallelism, keyed == 1);
        return true;
    }

    private static bool TryFromBase64(string value, out byte[] bytes)
    {
        var buffer = new byte[((value.Length * 3) + 3) / 4];
        if (Convert.TryFromBase64String(value, buffer, out var written))
        {
            bytes = buffer.Length == written ? buffer : buffer[..written];
            return true;
        }

        bytes = [];
        return false;
    }
}
