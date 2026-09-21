namespace KYC.TrueFace.Core.Domain.Constants;

public static class ValidationErrors
{
    public const string AuthIncorrectUserOrPassword = "login.validation.incorrectLogin";
    public const string AuthPasswordWeak = "login.validation.passwordWeak";
    public const string AuthIncorrectPasswordAndConfirmPassword = "login.validation.resetPassword";
    public const string AuthInvalidOrExpiredResetToken = "login.validation.invalidOrExpiredResetToken";
    public const string AuthAccountLocked = "login.validation.accountLocked";
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

    public const string UserReportInvalidPeriod = "users.report.validation.period";
    public const string UserReportFilterExceed = "users.report.validation.filterExceed";

    public const string OnboardingDocumentRequired = "onboarding.validation.documentRequired";
    public const string OnboardingSelfieRequired = "onboarding.validation.selfieRequired";
    public const string OnboardingImageEmpty = "onboarding.validation.imageEmpty";
    public const string OnboardingImageTooLarge = "onboarding.validation.imageTooLarge";
    public const string OnboardingImageContentTypeInvalid = "onboarding.validation.imageContentType";
    public const string OnboardingSituationInvalid = "onboarding.validation.situation";
    public const string OnboardingStorageNotConfigured = "onboarding.validation.storageNotConfigured";
    public const string OnboardingImageNotFound = "onboarding.validation.imageNotFound";
    public const string OnboardingImageKindInvalid = "onboarding.validation.imageKind";
    public const string OnboardingNameNullOrEmpty = "onboarding.validation.nameEmpty";
    public const string OnboardingNameExceed = "onboarding.validation.nameExceed";
    public const string OnboardingInvalidIdNumber = "onboarding.validation.idNumber";
    public const string OnboardingNotExisted = "onboarding.validation.notExisted";
    public const string OnboardingNotPendingReview = "onboarding.validation.notPendingReview";
    public const string OnboardingObservationNullOrEmpty = "onboarding.validation.observationEmpty";
    public const string OnboardingObservationExceed = "onboarding.validation.observationExceed";

    public const string EmailRecipientNullOrEmpty = "email.validation.recipientEmpty";
    public const string EmailSubjectNullOrEmpty = "email.validation.subjectEmpty";
    public const string EmailBodyNullOrEmpty = "email.validation.bodyEmpty";
    public const string EmailAttachmentNameNullOrEmpty = "email.validation.attachmentNameEmpty";
    public const string EmailAttachmentEmpty = "email.validation.attachmentEmpty";

    public const string GenericError = "notifications.errorDefault";
}
