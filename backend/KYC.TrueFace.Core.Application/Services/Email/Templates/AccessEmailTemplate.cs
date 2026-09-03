using System.Net;
using KYC.TrueFace.Core.Domain.Constants;

namespace KYC.TrueFace.Core.Application.Services.Email.Templates;

public static class AccessEmailTemplate
{
    public const string FirstAccessSubject = "Bem-vindo(a) à KYC TrueFace — configure seu acesso";
    public const string PasswordResetSubject = "Redefinição de senha — KYC TrueFace";

    public static string FirstAccess(string recipientName, string actionUrl) =>
        Build(
            heading: "Vamos configurar seu acesso",
            greeting: Greeting(recipientName),
            intro: "Sua conta na plataforma KYC TrueFace foi criada. Para concluir seu primeiro acesso, " +
                   "defina uma senha pessoal clicando no botão abaixo.",
            buttonLabel: "Definir minha senha",
            actionUrl: actionUrl);

    public static string PasswordReset(string recipientName, string actionUrl) =>
        Build(
            heading: "Redefinição de senha",
            greeting: Greeting(recipientName),
            intro: "Recebemos uma solicitação para redefinir a senha da sua conta KYC TrueFace. " +
                   "Clique no botão abaixo para escolher uma nova senha. " +
                   "Se você não fez essa solicitação, ignore este e-mail.",
            buttonLabel: "Redefinir senha",
            actionUrl: actionUrl);

    private static string Greeting(string recipientName) =>
        string.IsNullOrWhiteSpace(recipientName)
            ? "Olá,"
            : $"Olá, {WebUtility.HtmlEncode(recipientName.Trim())},";

    private static string Build(string heading, string greeting, string intro, string buttonLabel, string actionUrl)
    {
        var url = WebUtility.HtmlEncode(actionUrl);
        var expiry = TokenDefaults.LifetimeHours == 1 ? "1 hora" : $"{TokenDefaults.LifetimeHours} horas";

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
                      <p style="margin:0 0 28px 0; font-family:'Segoe UI',Roboto,Helvetica,Arial,sans-serif; font-size:15px; line-height:1.6; color:#CBD5E1;">
                        {{intro}}
                      </p>

                      <table role="presentation" cellpadding="0" cellspacing="0" border="0">
                        <tr>
                          <td align="center" style="border-radius:9999px; background-color:#6366F1;">
                            <a href="{{url}}" style="display:inline-block; padding:13px 30px; border-radius:9999px; font-family:'Segoe UI',Roboto,Helvetica,Arial,sans-serif; font-size:15px; font-weight:600; color:#FFFFFF; text-decoration:none;">
                              {{buttonLabel}}
                            </a>
                          </td>
                        </tr>
                      </table>

                      <p style="margin:28px 0 6px 0; font-family:'Segoe UI',Roboto,Helvetica,Arial,sans-serif; font-size:13px; line-height:1.6; color:#94A3B8;">
                        Ou copie e cole este endereço no seu navegador:
                      </p>
                      <p style="margin:0 0 24px 0; font-family:'Segoe UI',Roboto,Helvetica,Arial,sans-serif; font-size:13px; line-height:1.6; word-break:break-all;">
                        <a href="{{url}}" style="color:#A5B4FC; text-decoration:underline;">{{url}}</a>
                      </p>

                      <table role="presentation" width="100%" cellpadding="0" cellspacing="0" border="0">
                        <tr><td style="border-top:1px solid #334155; font-size:0; line-height:0;">&nbsp;</td></tr>
                      </table>

                      <p style="margin:20px 0 0 0; font-family:'Segoe UI',Roboto,Helvetica,Arial,sans-serif; font-size:12px; line-height:1.6; color:#64748B;">
                        Por segurança, este link expira em {{expiry}}.
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
