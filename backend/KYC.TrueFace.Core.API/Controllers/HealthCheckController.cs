using KYC.TrueFace.Core.API.Controllers.Base;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KYC.TrueFace.Core.API.Controllers;

[AllowAnonymous]
[Route("api/health-check")]
public class HealthCheckController : BaseController
{
    [HttpGet]
    public IActionResult HealthCheck()
        => Ok();
}
