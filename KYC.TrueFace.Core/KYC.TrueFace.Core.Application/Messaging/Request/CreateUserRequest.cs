using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Application.Messaging.Request;

public record CreateUserRequest
{
    public required string Name { get; set; }
    public required string IdNumber { get; set; }
    public required string Email { get; set; }
    public Permission Permission { get; set; }
    public string? MotherName { get; set; }
    public DateOnly BirthDate { get; set; }

    public CreateUserDto ToDto()
        => new(
                Name,
                IdNumber,
                Email,
                Permission,
                MotherName,
                BirthDate
            );
}