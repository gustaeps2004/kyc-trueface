using ClosedXML.Excel;
using KYC.TrueFace.Core.Domain.Enums;

namespace KYC.TrueFace.Core.Application.Services.Report;

public class UserReportExcelGenerator : IUserReportExcelGenerator
{
    private static readonly string[] Headers =
    [
        "CPF",
        "Nome",
        "E-mail",
        "Permissão",
        "Situação",
        "Data de nascimento",
        "Data de inclusão (UTC)"
    ];

    private static readonly double[] Widths = [18, 40, 40, 16, 12, 20, 22];

    public byte[] Generate(IEnumerable<Domain.Entities.User> users)
    {
        using var workbook = new XLWorkbook();
        var worksheet = workbook.AddWorksheet("Usuarios");

        for (var i = 0; i < Headers.Length; i++)
        {
            worksheet.Cell(1, i + 1).Value = Headers[i];

            worksheet.Column(i + 1).Width = Widths[i];
        }

        var header = worksheet.Range(1, 1, 1, Headers.Length);
        header.Style.Font.Bold = true;
        header.Style.Font.FontColor = XLColor.White;
        header.Style.Fill.BackgroundColor = XLColor.FromHtml("#1E293B");

        var row = 1;

        foreach (var user in users)
        {
            row++;

            var idNumber = worksheet.Cell(row, 1);
            idNumber.Style.NumberFormat.Format = "@";
            idNumber.Value = FormatIdNumber(user.IdNumber);

            worksheet.Cell(row, 2).Value = user.Name;
            worksheet.Cell(row, 3).Value = user.Email;
            worksheet.Cell(row, 4).Value = PermissionLabel(user.Permission);
            worksheet.Cell(row, 5).Value = SituationLabel(user.Situation);

            var birthDate = worksheet.Cell(row, 6);
            birthDate.Value = DateTime.SpecifyKind(user.BirthDate, DateTimeKind.Unspecified);
            birthDate.Style.NumberFormat.Format = "dd/MM/yyyy";

            var inclusionDt = worksheet.Cell(row, 7);
            inclusionDt.Value = DateTime.SpecifyKind(user.InclusionDt, DateTimeKind.Unspecified);
            inclusionDt.Style.NumberFormat.Format = "dd/MM/yyyy HH:mm";
        }

        worksheet.SheetView.FreezeRows(1);
        worksheet.Range(1, 1, row, Headers.Length).SetAutoFilter();

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);

        return stream.ToArray();
    }

    private static string SituationLabel(Situation situation) => situation switch
    {
        Situation.Enabled => "Ativo",
        Situation.Disabled => "Inativo",
        _ => situation.ToString()
    };

    private static string PermissionLabel(Permission permission) => permission switch
    {
        Permission.Commun => "Comum",
        Permission.Administrator => "Administrador",
        Permission.Master => "Master",
        _ => permission.ToString()
    };

    private static string FormatIdNumber(string idNumber)
    {
        var digits = new string([.. idNumber.Where(char.IsDigit)]);

        return digits.Length == 11
            ? $"{digits[..3]}.{digits[3..6]}.{digits[6..9]}-{digits[9..]}"
            : idNumber;
    }
}
