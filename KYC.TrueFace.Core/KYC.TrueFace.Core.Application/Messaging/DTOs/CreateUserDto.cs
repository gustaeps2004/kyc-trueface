using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Application.Messaging.DTOs;

public class CreateUserDto(
    string name,
    string idNumber,
    string email,
    Permission permission, 
    string? motherName,
    DateOnly birthDate)
{
    public string Name { get; set; } = name;
    public string IdNumber { get; set; } = idNumber;
    public string Email { get; set; } = email;
    public Permission Permission { get; set; } = permission;
    public string? MotherName { get; set; } = motherName;
    public DateOnly BirthDate { get; set; } = birthDate;
}