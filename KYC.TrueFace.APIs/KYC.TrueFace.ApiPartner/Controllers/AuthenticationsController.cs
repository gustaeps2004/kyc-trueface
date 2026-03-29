using KYC.TrueFace.ApiPartner.Controllers.Base;
using KYC.TrueFace.ApiPartner.Exceptions;
using KYC.TrueFace.ApiPartner.Messaging.Base;
using KYC.TrueFace.ApiPartner.Messaging.Request;
using KYC.TrueFace.ApiPartner.Services.Sso;
using Microsoft.AspNetCore.Mvc;
 
namespace KYC.TrueFace.ApiPartner.Controllers;

public class AuthenticationsController(
    ILogger<AuthenticationsController> logger,
    ISsoService service) : BaseController
{

    [HttpPost("register-password")]
    public async Task<IActionResult> RegisterPasswordAsync(RegisterPasswordRequest registerRequest)
    {
        try
        {
            await service.RegisterPasswordAsync(registerRequest.ToDto());

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