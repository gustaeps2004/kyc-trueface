using KYC.TrueFace.Core.Domain.Constants;
using Microsoft.AspNetCore.Mvc;

namespace KYC.TrueFace.Core.API.Controllers.Base;

public class BaseController : ControllerBase
{
    protected Guid GetPartnerCode()
    {
        var strCodePartner = User.Claims.FirstOrDefault(c => c.Type == IdentityClaims.PartnerCode)!.Value;
        return Guid.Parse(strCodePartner);
    }

    protected string GetIp()
        => HttpContext.Connection.RemoteIpAddress?.ToString() 
            ?? "Unidentified IP";
}