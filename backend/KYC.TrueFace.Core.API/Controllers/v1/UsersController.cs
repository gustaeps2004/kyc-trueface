using Asp.Versioning;
using KYC.TrueFace.Core.API.Controllers.Base;
using KYC.TrueFace.Core.Application.Messaging.Request;
using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Application.Services.User;
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
        => Ok(userService.ListByPartner(GetPartnerCode(), filter ?? string.Empty));

    [HttpPost]
    public IActionResult Insert(CreateUserRequest request)
    {
        userService.Create(request.ToDto(), GetPartnerCode());
        return Created();
    }

    [HttpPut("{code:Guid}")]
    public IActionResult Update(UpdateUserRequest request, [FromRoute] Guid code)
    {
        userService.Update(request.ToDto(), code);
        return NoContent();
    }
}
