using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KYC.TrueFace.Core.Infra.Ioc.Configs;

public static class CorsConfig
{
    public static void ConfigureCors(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddCors(options =>
        {
            options.AddPolicy(name: configuration["CorsName"]!,
                policy =>
                {
                    policy.WithOrigins(configuration["URLFront"]!) 
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
        });
    }
}