using System.Net;

namespace KYC.TrueFace.Core.Application.Services.Email.Templates;

public static class UserReportEmailTemplate
{
    public const string Subject = "Relatório de usuários — KYC TrueFace";

    public static string Ready(string recipientName, int totalRows, DateTime generatedAtUtc) =>
        Build(
            heading: "Seu relatório de usuários está pronto",
            greeting: Greeting(recipientName),
            intro: "O relatório que você solicitou foi gerado e está anexado a este e-mail " +
                   "no formato Excel (.xlsx).",
            summary: $"{totalRows} registro(s) — gerado em {generatedAtUtc:dd/MM/yyyy HH:mm} (UTC).");

    private static string Greeting(string recipientName) =>
        string.IsNullOrWhiteSpace(recipientName)
            ? "Olá,"
            : $"Olá, {WebUtility.HtmlEncode(recipientName.Trim())},";

    private static string Build(string heading, string greeting, string intro, string summary)
    {
        return $$"""
        <!DOCTYPE html>
        <html lang="pt-BR">
        <head>
          <meta charset="utf-8">
          <meta name="viewport" content="width=device-width, initial-scale=1.0">
          <meta name="color-scheme" content="dark light">
          <meta name="supported-color-schemes" content="dark light">
          <title>{{heading}}</title>
        </head>
        <body style="margin:0; padding:0; background-color:#0F172A; -webkit-text-size-adjust:100%; -ms-text-size-adjust:100%;">
          <span style="display:none !important; visibility:hidden; opacity:0; height:0; width:0; overflow:hidden; mso-hide:all;">
            {{intro}}
          </span>
          <table role="presentation" width="100%" cellpadding="0" cellspacing="0" border="0" style="background-color:#0F172A;">
            <tr>
              <td align="center" style="padding:32px 16px;">
                <table role="presentation" width="520" cellpadding="0" cellspacing="0" border="0" style="width:520px; max-width:100%;">

                  <tr>
                    <td style="padding:0 8px 20px 8px; font-family:'Segoe UI',Roboto,Helvetica,Arial,sans-serif; font-size:18px; font-weight:600; letter-spacing:-0.3px; color:#F8FAFC;">
                      <span style="color:#A5B4FC;">KYC</span> TrueFace
                    </td>
                  </tr>

                  <tr>
                    <td style="background-color:#1E293B; border:1px solid #334155; border-radius:14px; padding:36px 32px;">
                      <h1 style="margin:0 0 18px 0; font-family:'Segoe UI',Roboto,Helvetica,Arial,sans-serif; font-size:22px; line-height:1.3; font-weight:600; color:#F8FAFC;">
                        {{heading}}
                      </h1>
                      <p style="margin:0 0 14px 0; font-family:'Segoe UI',Roboto,Helvetica,Arial,sans-serif; font-size:15px; line-height:1.6; color:#CBD5E1;">
                        {{greeting}}
                      </p>
                      <p style="margin:0 0 24px 0; font-family:'Segoe UI',Roboto,Helvetica,Arial,sans-serif; font-size:15px; line-height:1.6; color:#CBD5E1;">
                        {{intro}}
                      </p>

                      <table role="presentation" width="100%" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                          <td style="background-color:#0F172A; border:1px solid #334155; border-radius:10px; padding:14px 18px; font-family:'Segoe UI',Roboto,Helvetica,Arial,sans-serif; font-size:14px; line-height:1.6; color:#A5B4FC;">
                            {{summary}}
                          </td>
                        </tr>
                      </table>

                      <table role="presentation" width="100%" cellpadding="0" cellspacing="0" border="0">
                        <tr><td style="padding-top:24px; border-bottom:1px solid #334155; font-size:0; line-height:0;">&nbsp;</td></tr>
                      </table>

                      <p style="margin:20px 0 0 0; font-family:'Segoe UI',Roboto,Helvetica,Arial,sans-serif; font-size:12px; line-height:1.6; color:#64748B;">
                        Este relatório contém dados pessoais. Trate o arquivo com o cuidado devido.
                      </p>
                    </td>
                  </tr>

                  <tr>
                    <td style="padding:22px 8px 0 8px; font-family:'Segoe UI',Roboto,Helvetica,Arial,sans-serif; font-size:12px; line-height:1.6; color:#64748B;">
                      Esta é uma mensagem automática da plataforma KYC TrueFace. Não responda a este e-mail.
                    </td>
                  </tr>

                </table>
              </td>
            </tr>
          </table>
        </body>
        </html>
        """;
    }
}
