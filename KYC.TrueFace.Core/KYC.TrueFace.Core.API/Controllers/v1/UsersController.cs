using Asp.Versioning;
using KYC.TrueFace.Core.API.Controllers.Base;
using KYC.TrueFace.Core.Application.Messaging.Request;
using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Application.Messaging.Response.Base;
using KYC.TrueFace.Core.Application.Services.User;
using KYC.TrueFace.Core.Domain.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KYC.TrueFace.Core.API.Controllers.v1;

[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/user")]
public class UsersController(
    IUserService userService) : BaseController
{
    [HttpGet]
    public ActionResult<IEnumerable<UserResponse>> ListByPartner([FromQuery] string? filter)
    {
        try
        {
            return Ok(
                userService.ListByPartner(GetPartnerCode(), filter ?? string.Empty)
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

    [HttpPost]
    public IActionResult Insert(CreateUserRequest request)
    {
        try
        {
            userService.Create(
                request.ToDto(), 
                GetPartnerCode()
            );

            return Created();
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

    [HttpPut("{code:Guid}")]
    public IActionResult Update(
        UpdateUserRequest request, 
        [FromRoute] Guid code)
    {
        try
        {
            userService.Update(
                request.ToDto(), 
                code);

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