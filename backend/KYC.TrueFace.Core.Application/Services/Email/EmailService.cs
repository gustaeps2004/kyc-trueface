using KYC.TrueFace.Core.Application.Messaging.DTOs;
using KYC.TrueFace.Core.Domain.Options;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace KYC.TrueFace.Core.Application.Services.Email;

public class EmailService(
    IOptions<SmtpOptions> smtpOptions) : IEmailService
{
    public async Task SendAsync(
        SendEmailDto emailDto,
        CancellationToken ct = default)
    {
        emailDto.Validate();

        var smtp = smtpOptions.Value;

        if (string.IsNullOrWhiteSpace(smtp.Host))
            throw new InvalidOperationException(
                "SMTP não configurado. Preencha a seção 'Smtp' no appsettings ou as variáveis Smtp__*.");

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(smtp.FromName, smtp.FromEmail));
        message.To.Add(MailboxAddress.Parse(emailDto.To));
        message.Subject = emailDto.Subject;
        message.Body = new BodyBuilder { HtmlBody = emailDto.HtmlBody }.ToMessageBody();

        using var client = new SmtpClient();

        var secureOption = smtp.UseSsl
            ? SecureSocketOptions.SslOnConnect
            : SecureSocketOptions.StartTls;

        await client.ConnectAsync(smtp.Host, smtp.Port, secureOption, ct);

        if (!string.IsNullOrWhiteSpace(smtp.Username))
            await client.AuthenticateAsync(smtp.Username, smtp.Password, ct);

        await client.SendAsync(message, ct);
        await client.DisconnectAsync(true, ct);
    }
}
