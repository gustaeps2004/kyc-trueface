using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Enums;
using KYC.TrueFace.Core.Domain.Exceptions;

namespace KYC.TrueFace.Core.Application.Messaging.DTOs;

public class UpdateUserDto(
    string name, 
    Permission permission, 
    string? motherName, 
    Situation situation, 
    DateTime birthDate)
{
    public string Name { get; set; } = name;
    public Permission Permission { get; set; } = permission;
    public string? MotherName { get; set; } = motherName;
    public Situation Situation { get; set; } = situation;
    public DateTime BirthDate { get; set; } = birthDate;

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(Name))
            throw new KycException(ValidationErrors.UserNameNullOrEmpty);

        if (Name.Length > 150)
            throw new KycException(ValidationErrors.UserNameExceed);

        if (Permission is 0 || !Enum.IsDefined(typeof(Permission), Permission))
            throw new KycException(ValidationErrors.UserPermissionInvalid);

        if (Situation is 0 || !Enum.IsDefined(typeof(Situation), Situation))
            throw new KycException(ValidationErrors.UserSituationInvalid);

        if (BirthDate == default || BirthDate >= DateTime.Now)
            throw new KycException(ValidationErrors.UserBirthDatenvalid);
    }
}