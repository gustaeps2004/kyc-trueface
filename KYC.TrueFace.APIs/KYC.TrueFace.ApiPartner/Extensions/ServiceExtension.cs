using KYC.TrueFace.ApiPartner.Entities.Base;
using KYC.TrueFace.ApiPartner.Repositories.Base;
using KYC.TrueFace.ApiPartner.Repositories.UserAccess;
using KYC.TrueFace.ApiPartner.Services.Sso;

namespace KYC.TrueFace.ApiPartner.Extensions;

public static class ServiceExtension
{
    public static void AddInjections(this IServiceCollection service)
    {
        service.InjectionServices();
        service.InjectionRepositories();
    }

    private static void InjectionServices(this IServiceCollection service)
    {
        service.AddTransient<ISsoService, SsoService>();
    }

    private static void InjectionRepositories(this IServiceCollection service)
    {
        service.AddTransient<IBaseRepository, BaseRepository>();
        //service.AddTransient<IBaseRepository<EntityBase<Type, Type>>, BaseRepository<EntityBase<Type, Type>>>();
        service.AddTransient<IUserAccessRepository, UserAccessRepository>();
    }
}
