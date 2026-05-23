# KYC TrueFace

A full-stack KYC (Know Your Customer) platform for identity verification, built with ASP.NET Core 8 and React 19.

## Table of Contents

- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Environment Variables](#environment-variables)
- [Running the Backend](#running-the-backend)
- [Running the Frontend](#running-the-frontend)
- [Running with Docker](#running-with-docker)
- [Database Migrations](#database-migrations)
- [API Documentation](#api-documentation)
- [Deployment](#deployment)

---

## Tech Stack

| Layer | Technology |
|---|---|
| Frontend | React 19, Vite 7, Tailwind CSS 4 |
| Backend | ASP.NET Core 8, C# |
| Database | PostgreSQL 16 |
| ORM | Entity Framework Core 8 |
| Auth | JWT Bearer + Argon2 |
| i18n | i18next (en / pt-BR) |
| Frontend deploy | Vercel via GitHub Actions |
| Backend deploy | Hostinger using Docker via GitHub Actions |

---

### Backend
- [.NET SDK 8.0](https://dotnet.microsoft.com/download/dotnet/8.0)
- PostgreSQL 16 (local or remote instance)

### Frontend
- [Node.js 20+](https://nodejs.org/)
- npm (bundled with Node.js)

### Docker (optional)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/)

---

## Environment Variables

### Backend

The API reads environment variables prefixed with `KYC_`. Set the following before running:

| Variable | Description | Example |
|---|---|---|
| `StrConn` | PostgreSQL connection string | `Host=localhost;Port=5432;Database=KYC_TRUEFACE;Username=postgres;Password=secret` |
| `URLFront` | Frontend URL for CORS | `http://localhost:5173` |
| `CorsName` | CORS policy name | `front_onboarding` |
| `SSO__Key` | JWT signing secret key | `your-256-bit-secret` |
| `SSO__Issuer` | JWT issuer | `KYC.TrueFace.Core.API` |
| `SSO__Audience` | JWT audience | `KYC.TrueFace.Web.WebPartner` |

For local development these values are already pre-configured in `launchSettings.json`.

### Frontend

Create a `.env` file inside `KYC.TrueFace.Web/KYC.TrueFace.WebPartner/` based on the provided template:

```bash
cp KYC.TrueFace.Web/KYC.TrueFace.WebPartner/.example.env \
   KYC.TrueFace.Web/KYC.TrueFace.WebPartner/.env
```

Then set the value:

```env
VITE_URL_API_BASE=https://localhost:7065/api
```

> Replace the URL with your backend address if running remotely.

---

## Running the Backend

### Option 1 — .NET CLI

```bash
# Navigate to the solution directory
cd KYC.TrueFace.Core

# Restore dependencies
dotnet restore

# Run the API (HTTPS on port 7065, HTTP on port 5184)
dotnet run --project KYC.TrueFace.Core.API/KYC.TrueFace.Core.API.csproj
```

The API will be available at:
- HTTPS: `https://localhost:7065`
- HTTP: `http://localhost:5184`

### Option 2 — Visual Studio

1. Open `KYC.TrueFace.Core/KYC.TrueFace.Core.sln` in Visual Studio 2022.
2. Set `KYC.TrueFace.Core.API` as the startup project.
3. Select the `https` launch profile.
4. Press **F5** to run with debugger or **Ctrl+F5** without.

---

## Running the Frontend

```bash
# Navigate to the frontend directory
cd KYC.TrueFace.Web/KYC.TrueFace.WebPartner

# Install dependencies
npm install

# Start the development server (port 5173)
npm run dev
```

The application will be available at `http://localhost:5173`.

### Other available scripts

| Command | Description |
|---|---|
| `npm run dev` | Start development server with hot-reload |
| `npm run build` | Build optimized production bundle |
| `npm run preview` | Serve the production build locally |

---

## Running with Docker

The backend includes a multi-stage `Dockerfile` for containerized deployments.

### Build the image

```bash
# Run from inside KYC.TrueFace.Core/
cd KYC.TrueFace.Core

docker build -f KYC.TrueFace.Core.API/Dockerfile -t kyc-trueface-api .
```

### Run the container

```bash
docker run -d \
  -p 8080:8080 \
  -e StrConn="Host=host.docker.internal;Port=5432;Database=KYC_TRUEFACE;Username=postgres;Password=secret" \
  -e URLFront="http://localhost:5173" \
  -e CorsName="front_onboarding" \
  -e SSO__Key="your-256-bit-secret" \
  -e SSO__Issuer="KYC.TrueFace.Core.API" \
  -e SSO__Audience="KYC.TrueFace.Web.WebPartner" \
  --name kyc-trueface-api \
  kyc-trueface-api
```

The API will be available at `http://localhost:8080`.

> **Note:** Use `host.docker.internal` in `StrConn` to reach a PostgreSQL instance running on your host machine from inside the container.

---

## Database Migrations

Migrations are managed by Entity Framework Core. Run all commands from the solution root.

```bash
cd KYC.TrueFace.Core

# Apply pending migrations to the database
dotnet ef database update \
  --project KYC.TrueFace.Core.Infra.Data/KYC.TrueFace.Core.Infra.Data.csproj \
  --startup-project KYC.TrueFace.Core.API/KYC.TrueFace.Core.API.csproj

# Create a new migration after changing domain entities
dotnet ef migrations add <MigrationName> \
  --project KYC.TrueFace.Core.Infra.Data/KYC.TrueFace.Core.Infra.Data.csproj \
  --startup-project KYC.TrueFace.Core.API/KYC.TrueFace.Core.API.csproj
```

> Ensure the `StrConn` environment variable is set to a reachable PostgreSQL instance before running migrations.

---

## API Documentation

Swagger UI is available in development mode at:

```
https://localhost:7065/swagger
```

---

## License

MIT © 2026 Gustavo Do Espirito Santo
