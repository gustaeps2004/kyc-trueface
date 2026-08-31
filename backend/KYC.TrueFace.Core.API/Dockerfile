# Acesse https://aka.ms/customizecontainer para saber como personalizar seu contêiner de depuração e como o Visual Studio usa este Dockerfile para criar suas imagens para uma depuração mais rápida.

# Esta fase é usada durante a execução no VS no modo rápido (Padrão para a configuração de Depuração)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase é usada para compilar o projeto de serviço
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["KYC.TrueFace.Core.API/KYC.TrueFace.Core.API.csproj", "KYC.TrueFace.Core.API/"]
COPY ["KYC.TrueFace.Core.Infra.Ioc/KYC.TrueFace.Core.Infra.Ioc.csproj", "KYC.TrueFace.Core.Infra.Ioc/"]
COPY ["KYC.TrueFace.Core.Application/KYC.TrueFace.Core.Application.csproj", "KYC.TrueFace.Core.Application/"]
COPY ["KYC.TrueFace.Core.Domain/KYC.TrueFace.Core.Domain.csproj", "KYC.TrueFace.Core.Domain/"]
COPY ["KYC.TrueFace.Core.Infra.Data/KYC.TrueFace.Core.Infra.Data.csproj", "KYC.TrueFace.Core.Infra.Data/"]
RUN dotnet restore "./KYC.TrueFace.Core.API/KYC.TrueFace.Core.API.csproj"
COPY . .
WORKDIR "/src/KYC.TrueFace.Core.API"
RUN dotnet build "./KYC.TrueFace.Core.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase é usada para publicar o projeto de serviço a ser copiado para a fase final
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./KYC.TrueFace.Core.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase é usada na produção ou quando executada no VS no modo normal (padrão quando não está usando a configuração de Depuração)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KYC.TrueFace.Core.API.dll"]