using KYC.TrueFace.Core.Domain.Entities.Base;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Domain.Entities;

public class UserAccessLog : EntityBase
{
    public Guid CodeUserAccess { get; set; }
    public DateTime SituationDt { get; set; }
    public FlowIdentity Flow { get; set; }
    public string Ip { get; set; } = null!;

    public virtual UserAccess? UserAccess { get; set; }

    public UserAccessLog() {  }

    public UserAccessLog(
        Guid codeUserAccess, 
        FlowIdentity flow, 
        string ip)
    {
        Code = Guid.NewGuid();
        InclusionDt = DateTime.UtcNow;
        CodeUserAccess = codeUserAccess;
        SituationDt = DateTime.UtcNow;
        Flow = flow;
        Ip = ip;
    }
}