namespace KYC.TrueFace.Core.Application.Services.Onboarding;

/// <summary>
/// Turns a PDF document (e.g. the CNH-e) into an image the face comparison can read.
/// Both methods throw <see cref="Domain.Exceptions.KycException"/> when the PDF cannot be
/// opened (corrupted, password protected or without pages).
/// </summary>
public interface IPdfRenderer
{
    void EnsureReadable(byte[] pdf);

    byte[] RenderFirstPageAsJpeg(byte[] pdf);
}
