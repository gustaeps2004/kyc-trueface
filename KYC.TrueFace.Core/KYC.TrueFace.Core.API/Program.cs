using KYC.TrueFace.Core.Infra.Data.Data;
using KYC.TrueFace.Core.Infra.Ioc.Configs;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration
    .AddEnvironmentVariables()
    .AddEnvironmentVariables("KYC_")
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

var connectionString = builder.Configuration["StrConn"];

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.ConfigureCors(builder.Configuration);

builder.Services.ConfigureDependencyInjection();
builder.Services.ConfigureVersioning();
builder.Services.TokenjwtConfiguration(builder.Configuration);

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

app.UseCors(builder.Configuration["CorsName"]!);

app.UseHttpsRedirection();

app.UseAuthentication(); 
app.UseAuthorization();

app.MapControllers();

app.Run();