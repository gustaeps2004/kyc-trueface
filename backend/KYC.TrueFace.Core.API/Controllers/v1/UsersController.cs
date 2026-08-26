using Asp.Versioning;
using KYC.TrueFace.Core.API.Controllers.Base;
using KYC.TrueFace.Core.Application.Messaging.Request;
using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Application.Services.User;
using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Enums;
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

    [Authorize(Roles = Roles.AdministratorOrMaster)]
    [HttpPost]
    public IActionResult Insert(CreateUserRequest request)
    {
        if (request.Permission == Permission.Master && !User.IsInRole(Roles.Master))
            return Forbid();

        userService.Create(request.ToDto(), GetPartnerCode());
        return Created();
    }

    [Authorize(Roles = Roles.AdministratorOrMaster)]
    [HttpPut("{code:Guid}")]
    public IActionResult Update(UpdateUserRequest request, [FromRoute] Guid code)
    {
        if (request.Permission == Permission.Master && !User.IsInRole(Roles.Master))
            return Forbid();

        userService.Update(request.ToDto(), code, GetPartnerCode());
        return NoContent();
    }
}
