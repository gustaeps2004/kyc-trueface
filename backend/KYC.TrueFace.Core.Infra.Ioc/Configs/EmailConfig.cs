using KYC.TrueFace.Core.Application.Services.Email;
using KYC.TrueFace.Core.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KYC.TrueFace.Core.Infra.Ioc.Configs;

public static class EmailConfig
{
    public static void ConfigureEmail(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<SmtpOptions>(configuration.GetSection("Smtp"));
        services.AddTransient<IEmailService, EmailService>();
    }
}
