using Asp.Versioning;
using KYC.TrueFace.Core.API.Controllers.Base;
using KYC.TrueFace.Core.Application.Messaging.Request;
using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Application.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KYC.TrueFace.Core.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/auth")]
public class AuthController(
    IAuthenticateService authenticateService) : BaseController
{
    [AllowAnonymous]
    [HttpPost]
    public ActionResult<AuthenticateLoginResponse> Login(LoginRequest request)
        => Ok(authenticateService.Login(request.ToDto(), GetIp()));

    [AllowAnonymous]
    [HttpPost("reset-password")]
    public IActionResult ResetPassword(ResetPasswordRequest request)
    {
        authenticateService.ResetPassword(request.ToDto(), GetIp());
        return NoContent();
    }
}
