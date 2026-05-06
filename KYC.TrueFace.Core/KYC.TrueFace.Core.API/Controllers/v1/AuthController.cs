using Asp.Versioning;
using KYC.TrueFace.Core.API.Controllers.Base;
using KYC.TrueFace.Core.Application.Messaging.Request;
using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Application.Messaging.Response.Base;
using KYC.TrueFace.Core.Application.Services.Auth;
using KYC.TrueFace.Core.Domain.Exceptions;
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
    [HttpPost("auth")]
    public ActionResult<AuthenticateLoginResponse> Login(LoginRequest request)
    {
        try
        {
            return Ok(
                authenticateService.Login(request.ToDto(), GetIp())
            );
        }
        catch (KycException ex)
        {
            return BadRequest(
                new BaseResponse(ex.Message)
            );
        }
        catch
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new ResponseError()
            );
        }
    }

    [AllowAnonymous]
    [HttpPost("reset-password")]
    public IActionResult ResetPassword(ResetPasswordRequest request)
    {
        try
        {
            authenticateService.ResetPassword(
                request.ToDto(),
                GetIp()
            );

            return NoContent();
        }
        catch (KycException ex)
        {
            return BadRequest(
                new BaseResponse(ex.Message)
            );
        }
        catch
        {
            return StatusCode(
                StatusCodes.Status500InternalServerError,
                new ResponseError()
            );
        }
    }
}