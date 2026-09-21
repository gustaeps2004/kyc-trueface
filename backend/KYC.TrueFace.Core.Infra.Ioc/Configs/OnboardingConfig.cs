using KYC.TrueFace.Core.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KYC.TrueFace.Core.Infra.Ioc.Configs;

public static class OnboardingConfig
{
    public static void ConfigureOnboarding(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<OnboardingOptions>(
            configuration.GetSection(OnboardingOptions.SectionName));
    }
}
