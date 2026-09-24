using KYC.TrueFace.Core.Application.Services.Auth;
using KYC.TrueFace.Core.Application.Services.Dashboard;
using KYC.TrueFace.Core.Application.Services.FaceComparison;
using KYC.TrueFace.Core.Application.Services.Onboarding;
using KYC.TrueFace.Core.Application.Services.Report;
using KYC.TrueFace.Core.Application.Services.Token;
using KYC.TrueFace.Core.Application.Services.User;
using KYC.TrueFace.Core.Application.Services.UserAccess;
using KYC.TrueFace.Core.Domain.Repositories;
using KYC.TrueFace.Core.Infra.Data.Repositories.Base;
using KYC.TrueFace.Core.Infra.Data.Repositories.Onboardings;
using KYC.TrueFace.Core.Infra.Data.Repositories.User;
using KYC.TrueFace.Core.Infra.Data.Repositories.UsersAccess;
using KYC.TrueFace.Core.Infra.Data.Repositories.UsersReports;
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
        services.AddTransient<IUserReportService, UserReportService>();
        services.AddTransient<IUserReportProcessorService, UserReportProcessorService>();
        services.AddTransient<IUserReportExcelGenerator, UserReportExcelGenerator>();
        services.AddTransient<IOnboardingService, OnboardingService>();
        services.AddTransient<IOnboardingProcessorService, OnboardingProcessorService>();
        services.AddTransient<IOnboardingImageStorage, OnboardingImageStorage>();
        services.AddTransient<IFaceComparisonService, RekognitionFaceComparisonService>();
        services.AddTransient<IDashboardService, DashboardService>();

        // PDFium, behind PdfRenderer, only ships native binaries for these platforms.
        if (OperatingSystem.IsWindows() || OperatingSystem.IsLinux() || OperatingSystem.IsMacOS())
            services.AddSingleton<IPdfRenderer, PdfRenderer>();
    }

    private static void ConfigureRepositories(this IServiceCollection services)
    {
        services.AddTransient<IBaseRepository, BaseRepository>();
        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IUserAccessRepository, UserAccessRepository>();
        services.AddTransient<IUserReportRepository, UserReportRepository>();
        services.AddTransient<IOnboardingRepository, OnboardingRepository>();
    }
}
