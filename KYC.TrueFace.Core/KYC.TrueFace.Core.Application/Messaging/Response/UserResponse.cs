using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Application.Messaging.Response;

public class UserResponse
{
    public Guid Code { get; set; }
    public DateTime InclusionDt { get; set; }
    public string Name { get; set; } = null!;
    public string IdNumber { get; set; } = null!;
    public DateTime BirthDate { get; set; }
    public string? MotherName { get; set; }
    public string Email { get; set; } = null!;
    public Permission Permission { get; set; }
    public Situation Situation { get; set; }
}