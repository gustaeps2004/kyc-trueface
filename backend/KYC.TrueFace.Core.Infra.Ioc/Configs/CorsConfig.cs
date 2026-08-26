using KYC.TrueFace.Core.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KYC.TrueFace.Core.Infra.Ioc.Configs;

public static class CorsConfig
{
    public static void ConfigureCors(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var section = configuration.GetSection("App");
        var appOptions = section.Get<AppOptions>()!;

        services.Configure<AppOptions>(section);

        services.AddCors(options =>
        {
            options.AddPolicy(name: appOptions.CorsName,
                policy =>
                {
                    policy.WithOrigins(appOptions.FrontendUrl)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
        });
    }
}