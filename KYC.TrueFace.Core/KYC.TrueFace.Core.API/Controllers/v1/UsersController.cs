using Asp.Versioning;
using KYC.TrueFace.Core.API.Controllers.Base;
using KYC.TrueFace.Core.Application.Messaging.Request;
using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Application.Messaging.Response.Base;
using KYC.TrueFace.Core.Application.Services.User;
using KYC.TrueFace.Core.Domain.Entities;
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
    public ActionResult<IEnumerable<User>> ListByPartner()
    {
        try
        {
            var response = userService.ListByPartner(GetPartnerCode());

            return Ok(response);
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
}