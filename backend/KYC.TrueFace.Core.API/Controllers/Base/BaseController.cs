using System.Security.Claims;
using KYC.TrueFace.Core.Domain.Constants;
using KYC.TrueFace.Core.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace KYC.TrueFace.Core.API.Controllers.Base;

public class BaseController : ControllerBase
{
    protected Guid GetPartnerCode()
    {
        var strCodePartner = User.Claims.FirstOrDefault(c => c.Type == IdentityClaims.PartnerCode)!.Value;
        return Guid.Parse(strCodePartner);
    }

    protected string GetUsername()
        => User.FindFirst("sub")?.Value
           ?? User.FindFirst(ClaimTypes.NameIdentifier)?.Value
           ?? throw new KycException(ValidationErrors.AuthIncorrectUserOrPassword);

    protected string GetIp()
        => HttpContext.Connection.RemoteIpAddress?.ToString()
            ?? "Unidentified IP";
}
