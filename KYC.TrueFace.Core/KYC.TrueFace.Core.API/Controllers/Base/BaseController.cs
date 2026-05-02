using Microsoft.AspNetCore.Mvc;

namespace KYC.TrueFace.Core.API.Controllers.Base;

public class BaseController : ControllerBase
{
    public Guid GetPartnerCode()
        => Guid.Parse("780ebffa-ef6f-4cf8-b9d2-d2b54bdaf63b");
}