using KYC.TrueFace.ApiPartner.Consts;
using KYC.TrueFace.ApiPartner.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KYC.TrueFace.ApiPartner.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    public Guid GetUserCode()
    {
        var isGuidValid = Guid.TryParse(User.FindFirstValue(SsoClaims.UserCode), out var userCode);

        return isGuidValid
            ? userCode
            : throw new TrueFaceException("Was not possible to find user code.");
    }
}