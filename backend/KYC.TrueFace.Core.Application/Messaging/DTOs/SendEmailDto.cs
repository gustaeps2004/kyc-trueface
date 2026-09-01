using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Exceptions;

namespace KYC.TrueFace.Core.Application.Messaging.DTOs;

public class SendEmailDto(
    string to,
    string subject,
    string htmlBody)
{
    public string To { get; set; } = to;
    public string Subject { get; set; } = subject;
    public string HtmlBody { get; set; } = htmlBody;

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(To))
            throw new KycException(ValidationErrors.EmailRecipientNullOrEmpty);

        if (string.IsNullOrWhiteSpace(Subject))
            throw new KycException(ValidationErrors.EmailSubjectNullOrEmpty);

        if (string.IsNullOrWhiteSpace(HtmlBody))
            throw new KycException(ValidationErrors.EmailBodyNullOrEmpty);
    }
}
