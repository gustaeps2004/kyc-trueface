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
    [Authorize(Roles = Roles.AllAccess)]
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserResponse>>> ListByPartnerAsync([FromQuery] string? filter, CancellationToken ct)
        => Ok(await userService.ListByPartnerAsync(GetPartnerCode(), filter ?? string.Empty, ct));

    [Authorize(Roles = Roles.AdministratorOrMaster)]
    [HttpPost]
    public async Task<IActionResult> InsertAsync(CreateUserRequest request, CancellationToken ct)
    {
        if (request.Permission == Permission.Master && !User.IsInRole(Roles.Master))
            return Forbid();

        await userService.CreateAsync(request.ToDto(), GetPartnerCode(), ct);
        return Created();
    }

    [Authorize(Roles = Roles.AdministratorOrMaster)]
    [HttpPut("{code:Guid}")]
    public async Task<IActionResult> UpdateAsync(UpdateUserRequest request, [FromRoute] Guid code, CancellationToken ct)
    {
        if (request.Permission == Permission.Master && !User.IsInRole(Roles.Master))
            return Forbid();

        await userService.UpdateAsync(request.ToDto(), code, GetPartnerCode(), ct);
        return NoContent();
    }
}
