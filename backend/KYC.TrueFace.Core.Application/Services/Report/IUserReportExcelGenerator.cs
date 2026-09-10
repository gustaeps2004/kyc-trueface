namespace KYC.TrueFace.Core.Application.Services.Report;

public interface IUserReportExcelGenerator
{
    byte[] Generate(IEnumerable<Domain.Entities.User> users);
}
