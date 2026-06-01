using System.Text.Json.Serialization;
using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Application.Messaging.Request;

public sealed record UpdateUserRequest
{
    public required string Name { get; set; }
    public Permission Permission { get; set; }
    public string? MotherName { get; set; }
    public Situation Situation { get; set; }

    [JsonPropertyName("bithDate")]
    public DateTime BirthDate { get; set; }

    public UpdateUserDto ToDto()
        => new(
            Name,
            Permission,
            MotherName,
            Situation,
            BirthDate);
}