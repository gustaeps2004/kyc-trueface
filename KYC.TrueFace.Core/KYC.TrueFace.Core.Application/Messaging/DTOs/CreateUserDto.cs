using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.Exceptions;
using KYC.TrueFace.Core.Domain.Extensions;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace KYC.TrueFace.Core.Application.Messaging.DTOs;

public class CreateUserDto(
    string name,
    string idNumber,
    string email,
    Permission permission,
    string? motherName,
    DateTime birthDate)
{
    public string Name { get; set; } = name;
    public string IdNumber { get; set; } = idNumber;
    public string Email { get; set; } = email;
    public Permission Permission { get; set; } = permission;
    public string? MotherName { get; set; } = motherName;
    public DateTime BirthDate { get; set; } = birthDate;

    public void Validate()
    {
        if (Name.Length > 150)
            throw new KycException("The name cannot exceed 150 characters.");

        if (ValidationsExtension.IsIdNumberInvalid(IdNumber))
            throw new KycException("Invalid id number.");

        if (Email.Length > 150)
            throw new KycException("The e-mail cannot exceed 150 characters.");

        if (!Enum.IsDefined(typeof(Permission), Permission))
            throw new KycException("Invalid permission.");

        if (BirthDate == default || BirthDate >= DateTime.Now)
            throw new KycException("Invalid birth date.");
    }
}