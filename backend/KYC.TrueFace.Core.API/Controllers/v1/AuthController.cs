using Asp.Versioning;
using KYC.TrueFace.Core.API.Controllers.Base;
using KYC.TrueFace.Core.Application.Messaging.Request;
using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Application.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace KYC.TrueFace.Core.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
[EnableRateLimiting(RateLimitPolicies.Login)]
public class AuthController(
    IAuthenticateService authenticateService) : BaseController
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult<AuthenticateLoginResponse>> LoginAsync(LoginRequest request, CancellationToken ct)
        => Ok(await authenticateService.LoginAsync(request.ToDto(), GetIp(), ct));

    [Authorize]
    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync(ResetPasswordRequest request, CancellationToken ct)
    {
        await authenticateService.ResetPasswordAsync(request.ToDto(), GetIp(), ct);
        return NoContent();
    }

    [AllowAnonymous]
    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPasswordAsync(ForgotPasswordRequest request, CancellationToken ct)
    {
        await authenticateService.ForgotPasswordAsync(request.ToDto(), ct);
        return NoContent();
    }
}
