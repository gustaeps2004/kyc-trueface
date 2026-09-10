using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Exceptions;

namespace KYC.TrueFace.Core.Application.Messaging.DTOs;

public class EmailAttachmentDto(
    string fileName,
    byte[] content,
    string contentType)
{
    public string FileName { get; set; } = fileName;
    public byte[] Content { get; set; } = content;
    public string ContentType { get; set; } = contentType;

    public void Validate()
    {
        if (string.IsNullOrWhiteSpace(FileName))
            throw new KycException(ValidationErrors.EmailAttachmentNameNullOrEmpty);

        if (Content is null || Content.Length == 0)
            throw new KycException(ValidationErrors.EmailAttachmentEmpty);
    }
}
