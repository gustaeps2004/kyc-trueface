using Asp.Versioning;
using KYC.TrueFace.Core.API.Controllers.Base;
using KYC.TrueFace.Core.Application.Messaging.Response;
using KYC.TrueFace.Core.Application.Services.Dashboard;
using KYC.TrueFace.Core.Domain.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KYC.TrueFace.Core.API.Controllers.v1;

[ApiController]
[Authorize]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/dashboard")]
public class DashboardController(
    IDashboardService dashboardService) : BaseController
{
    /// <summary>Counters for the dashboard cards, scoped to the partner of the logged user.</summary>
    [Authorize(Roles = Roles.AllAccess)]
    [HttpGet("summary")]
    public async Task<ActionResult<DashboardSummaryResponse>> GetSummaryAsync(CancellationToken ct)
        => Ok(await dashboardService.GetSummaryAsync(GetPartnerCode(), ct));
}
