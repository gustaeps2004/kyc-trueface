using System.Runtime.Versioning;
using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Exceptions;
using PDFtoImage;
using PDFtoImage.Exceptions;
using SkiaSharp;

namespace KYC.TrueFace.Core.Application.Services.Onboarding;

/// <summary>Renders PDFs with PDFium, which PDFtoImage ships for these platforms only.</summary>
[SupportedOSPlatform("windows")]
[SupportedOSPlatform("linux")]
[SupportedOSPlatform("macos")]
public class PdfRenderer : IPdfRenderer
{
    // Longest side of the rendered page. On an A4 page it keeps the document face well above
    // the size Rekognition needs, while bounding memory for PDFs with oversized pages and
    // keeping the JPEG far below the 5 MB Rekognition accepts.
    private const int MaxRenderDimension = 2400;

    private const int JpegQuality = 90;

    public void EnsureReadable(byte[] pdf)
    {
        if (Guard(() => Conversion.GetPageCount(pdf)) < 1)
            throw new KycException(ValidationErrors.OnboardingDocumentPdfUnreadable);
    }

    public byte[] RenderFirstPageAsJpeg(byte[] pdf)
        => Guard(() =>
        {
            var size = Conversion.GetPageSize(pdf, page: 0);
            var landscape = size.Width > size.Height;

            var options = new RenderOptions(
                Width: landscape ? MaxRenderDimension : null,
                Height: landscape ? null : MaxRenderDimension,
                WithAspectRatio: true,
                BackgroundColor: SKColors.White);

            using var bitmap = Conversion.ToImage(pdf, page: 0, options: options);
            using var jpeg = bitmap.Encode(SKEncodedImageFormat.Jpeg, JpegQuality);

            return jpeg.ToArray();
        });

    private static T Guard<T>(Func<T> action)
    {
        try
        {
            return action();
        }
        catch (PdfException)
        {
            throw new KycException(ValidationErrors.OnboardingDocumentPdfUnreadable);
        }
    }
}
