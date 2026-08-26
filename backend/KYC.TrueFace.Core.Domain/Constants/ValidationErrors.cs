namespace KYC.TrueFace.Core.Domain.Constants;

public static class ValidationErrors
{
    public const string AuthIncorrectUserOrPassword = "login.validation.incorrectLogin";
    public const string AuthPasswordWeak = "login.validation.passwordWeak";
    public const string AuthIncorrectPasswordAndConfirmPassword = "login.validation.resetPassword";
    public const string AuthInvalidOrExpiredResetToken = "login.validation.invalidOrExpiredResetToken";
    public const string UserExisted = "users.validation.existed";
    public const string UserNotExisted = "users.validation.notExisted";
    public const string UserPasswordCannotBeNullOrEmpty = "users.validation.password";
    public const string UserInvalidIdNumber = "users.validation.idNumber";
    public const string UserNameNullOrEmpty = "users.validation.nameEmpty";
    public const string UserNameExceed = "users.validation.nameExceed";    
    public const string UserEmailNullOrEmpty = "users.validation.emailEmpty";
    public const string UserEmailExceed = "users.validation.emailExceed";
    public const string UserPermissionInvalid = "users.validation.permission";
    public const string UserSituationInvalid = "users.validation.situation";
    public const string UserBirthDatenvalid = "users.validation.birthDate";

    public const string GenericError = "notifications.errorDefault";
}