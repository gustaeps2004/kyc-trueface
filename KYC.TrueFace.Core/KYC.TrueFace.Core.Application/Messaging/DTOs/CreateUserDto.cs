using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.Exceptions;
using KYC.TrueFace.Core.Domain.Extensions;

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
        if (string.IsNullOrWhiteSpace(Name))
            throw new KycException(ValidationErrors.UserNameNullOrEmpty);

        if (Name.Length > 150)
            throw new KycException(ValidationErrors.UserNameExceed);

        if (ValidationsExtension.IsIdNumberInvalid(IdNumber))
            throw new KycException(ValidationErrors.UserInvalidIdNumber);

        if (string.IsNullOrWhiteSpace(Email))
            throw new KycException(ValidationErrors.UserEmailNullOrEmpty);

        if (Email.Length > 150)
            throw new KycException(ValidationErrors.UserEmailExceed);

        if (!Enum.IsDefined(typeof(Permission), Permission))
            throw new KycException(ValidationErrors.UserPermissionInvalid);

        if (BirthDate == default || BirthDate >= DateTime.Now)
            throw new KycException(ValidationErrors.UserBirthDatenvalid);
    }
}