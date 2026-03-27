using KYC.TrueFace.ApiPartner.Controllers.Base;
using KYC.TrueFace.ApiPartner.Exceptions;
using KYC.TrueFace.ApiPartner.Messaging.Base;
using Microsoft.AspNetCore.Mvc;

namespace KYC.TrueFace.ApiPartner.Controllers;

public class AuthenticationsController(
    ILogger<AuthenticationsController> logger) : BaseController
{

    [HttpPost("register-password")]
    public IActionResult RegisterPassword()
    {
        try
        {
            return Ok();
        }
        catch (TrueFaceException ex)
        {
            return BadRequest(ResponseBase.SetError(ex));
        }
        catch (Exception exc)
        {
            logger.LogError(exc.Message);

            return StatusCode(
                StatusCodes.Status500InternalServerError, 
                ResponseBase.GenericError());
        }
    }
}