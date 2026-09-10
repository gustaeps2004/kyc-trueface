using KYC.TrueFace.Core.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KYC.TrueFace.Core.Infra.Ioc.Configs;

public static class UserReportConfig
{
    public static void ConfigureUserReport(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<UserReportOptions>(
            configuration.GetSection(UserReportOptions.SectionName));
    }
}
