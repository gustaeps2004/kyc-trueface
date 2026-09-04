using System.Threading.RateLimiting;
using KYC.TrueFace.Core.API;
using KYC.TrueFace.Core.API.Middlewares;
using KYC.TrueFace.Core.Domain.Options;
using KYC.TrueFace.Core.Infra.Data.Data;
using KYC.TrueFace.Core.Infra.Ioc.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder
    .Configuration
    .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.ConfigureCors(builder.Configuration);

builder.Services.ConfigureDependencyInjection();
builder.Services.ConfigureVersioning();
builder.Services.TokenjwtConfiguration(builder.Configuration);
builder.Services.ConfigureEmail(builder.Configuration);
builder.Services.ConfigurePasswordHashing(builder.Configuration);
builder.Services.ConfigureLoginSecurity(builder.Configuration);

var loginSecurity = builder.Configuration
    .GetSection(LoginSecurityOptions.SectionName)
    .Get<LoginSecurityOptions>() ?? new LoginSecurityOptions();

builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy(RateLimitPolicies.Login, httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = loginSecurity.RateLimitPermitLimit,
                Window = TimeSpan.FromSeconds(loginSecurity.RateLimitWindowSeconds),
                QueueLimit = 0
            }));
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var appOptions = app.Services.GetRequiredService<IOptions<AppOptions>>().Value;

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseCors(appOptions.CorsName);

app.UseHttpsRedirection();

app.UseRateLimiter();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();