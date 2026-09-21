using Asp.Versioning;
using KYC.TrueFace.Core.API.Controllers.Base;
using KYC.TrueFace.Core.Application.Messaging.Request;
using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Application.Services.Onboarding;
using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KYC.TrueFace.Core.API.Controllers.v1;

[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/onboarding")]
public class OnboardingsController(
    IOnboardingService onboardingService) : BaseController
{
    /// <summary>
    /// Uploads the document and the selfie and queues the pair for face comparison.
    /// </summary>
    [Authorize(Roles = Roles.AdministratorOrMaster)]
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<ActionResult<OnboardingResponse>> UploadAsync(
        [FromForm] CreateOnboardingRequest request,
        CancellationToken ct)
    {
        var response = await onboardingService.RegisterAsync(request.ToDto(), GetPartnerCode(), ct);

        return Accepted(response);
    }

    /// <summary>Onboardings the automatic validation could not settle, waiting for a human.</summary>
    [Authorize(Roles = Roles.AllAccess)]
    [HttpGet("manual-review")]
    public async Task<ActionResult<IEnumerable<OnboardingListItemResponse>>> ListPendingManualReviewAsync(
        CancellationToken ct)
        => Ok(await onboardingService.ListPendingManualReviewAsync(GetPartnerCode(), ct));

    /// <summary>Onboardings already approved or denied. Optionally narrowed to one of them.</summary>
    [Authorize(Roles = Roles.AllAccess)]
    [HttpGet("reviewed")]
    public async Task<ActionResult<IEnumerable<OnboardingListItemResponse>>> ListReviewedAsync(
        [FromQuery] OnboardingSituation? situation,
        CancellationToken ct)
        => Ok(await onboardingService.ListReviewedAsync(GetPartnerCode(), situation, ct));

    /// <summary>Serves one of the uploaded images, so a reviewer can inspect it.</summary>
    [Authorize(Roles = Roles.AllAccess)]
    [HttpGet("{code:Guid}/image/{kind}")]
    public async Task<IActionResult> GetImageAsync(
        [FromRoute] Guid code,
        [FromRoute] string kind,
        CancellationToken ct)
    {
        var image = await onboardingService.GetImageAsync(code, kind, GetPartnerCode(), ct);

        return File(image.Content, image.ContentType, image.FileName);
    }

    /// <summary>Approves or denies a record sitting in manual review.</summary>
    [Authorize(Roles = Roles.AdministratorOrMaster)]
    [HttpPost("{code:Guid}/review")]
    public async Task<IActionResult> ReviewAsync(
        [FromRoute] Guid code,
        ReviewOnboardingRequest request,
        CancellationToken ct)
    {
        await onboardingService.ReviewAsync(
            request.ToDto(),
            code,
            GetUserCode(),
            GetPartnerCode(),
            ct);

        return NoContent();
    }
}
