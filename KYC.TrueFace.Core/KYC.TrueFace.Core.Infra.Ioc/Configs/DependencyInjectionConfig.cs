using KYC.TrueFace.Core.Infra.Data.Repositories.Base;
using KYC.TrueFace.Core.Infra.Data.Repositories.User;
using Microsoft.Extensions.DependencyInjection;

namespace KYC.TrueFace.Core.Infra.Ioc.Configs;

public static class DependencyInjectionConfig
{
    public static void ConfigureDependencyInjection(this IServiceCollection services)
    {
        services.ConfigureServices();
        services.ConfigureRepositories();
    }

    private static void ConfigureServices(this IServiceCollection services)
    {

    }

    private static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddTransient<IBaseRepository, BaseRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
    }
}