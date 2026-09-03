using KYC.TrueFace.Core.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KYC.TrueFace.Core.Infra.Ioc.Configs;

public static class LoginSecurityConfig
{
    public static void ConfigureLoginSecurity(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<LoginSecurityOptions>(
            configuration.GetSection(LoginSecurityOptions.SectionName));
    }
}
