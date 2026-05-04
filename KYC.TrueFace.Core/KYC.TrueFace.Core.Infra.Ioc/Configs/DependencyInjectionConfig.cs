using KYC.TrueFace.Core.Application.Services.Auth;
using KYC.TrueFace.Core.Application.Services.Token;
using KYC.TrueFace.Core.Application.Services.User;
using KYC.TrueFace.Core.Application.Services.UserAccess;
using KYC.TrueFace.Core.Infra.Data.Repositories.Base;
using KYC.TrueFace.Core.Infra.Data.Repositories.User;
using KYC.TrueFace.Core.Infra.Data.Repositories.UsersAccess;
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
        services.AddTransient<IUserService, UserService>();
        services.AddTransient<ITokenService, TokenService>();
        services.AddTransient<IUserAccessService, UserAccessService>();
        services.AddTransient<IAuthenticateService, AuthenticateService>();
    }

    private static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddTransient<IBaseRepository, BaseRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserAccessRepository, UserAccessRepository>();
    }
}