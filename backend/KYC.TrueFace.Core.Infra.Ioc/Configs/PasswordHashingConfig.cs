using KYC.TrueFace.Core.Application.Security;
using KYC.TrueFace.Core.Domain.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace KYC.TrueFace.Core.Infra.Ioc.Configs;

public static class PasswordHashingConfig
{
    public static void ConfigurePasswordHashing(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.Configure<PasswordHashingOptions>(
            configuration.GetSection(PasswordHashingOptions.SectionName));

        services.AddSingleton<IPasswordHasher, Argon2PasswordHasher>();
    }
}
