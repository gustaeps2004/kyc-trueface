using KYC.TrueFace.Core.Domain.Entities.Base;
using KYC.TrueFace.Core.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace KYC.TrueFace.Core.Domain.Entities;

public class Partner : EntityBase
{
    public required string IdNumber { get; set; }
    public required string Name { get; set; }
    public required string Email { get; set; }
    public Situation Situation { get; set; }
}