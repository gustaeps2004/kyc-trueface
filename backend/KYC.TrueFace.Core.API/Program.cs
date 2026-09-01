using KYC.TrueFace.Core.API.Middlewares;
using KYC.TrueFace.Core.Domain.Options;
using KYC.TrueFace.Core.Infra.Data.Data;
using KYC.TrueFace.Core.Infra.Ioc.Configs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.ConfigureCors(builder.Configuration);

builder.Services.ConfigureDependencyInjection();
builder.Services.ConfigureVersioning();
builder.Services.TokenjwtConfiguration(builder.Configuration);
builder.Services.ConfigureEmail(builder.Configuration);

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

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();