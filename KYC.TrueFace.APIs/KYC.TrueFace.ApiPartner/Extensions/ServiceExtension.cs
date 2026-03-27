using KYC.TrueFace.ApiPartner.Services.Sso;

namespace KYC.TrueFace.ApiPartner.Extensions;

public static class ServiceExtension
{
    public static void AddInjection(this IServiceCollection service)
    {
        service.InsectionService();
    }

    private static void InsectionService(this IServiceCollection service)
    {
        service.AddTransient<ISsoService, SsoService>();
    }
}
