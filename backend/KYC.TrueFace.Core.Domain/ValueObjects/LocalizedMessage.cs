using System.Text.Json;

namespace KYC.TrueFace.Core.Domain.ValueObjects;

/// <summary>
/// A translation key plus the values interpolated into it, so the client renders the
/// message in the user's language instead of the one the server would have written it in.
/// </summary>
public sealed record LocalizedMessage(string Key, IReadOnlyDictionary<string, string>? Args = null)
{
    public string? SerializeArgs()
        => Args is { Count: > 0 } ? JsonSerializer.Serialize(Args) : null;

    public static IReadOnlyDictionary<string, string>? DeserializeArgs(string? json)
        => string.IsNullOrEmpty(json)
            ? null
            : JsonSerializer.Deserialize<Dictionary<string, string>>(json);
}
