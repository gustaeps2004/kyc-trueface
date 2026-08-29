# KYC TrueFace

A full-stack KYC (Know Your Customer) platform for identity verification, built with ASP.NET Core 8 and React 19.

## Table of Contents

- [Tech Stack](#tech-stack)
- [Environment Variables](#environment-variables)
- [Running the Backend](#running-the-backend)
- [Running the Frontend](#running-the-frontend)
- [Running with Docker](#running-with-docker)
- [Database Migrations](#database-migrations)
- [API Documentation](#api-documentation)

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
- [Docker official installation docs](https://docs.docker.com/get-docker/)

---

## Environment Variables

### Backend

Configuration lives in standard ASP.NET Core `appsettings.json` files inside `backend/KYC.TrueFace.Core.API/`:

- **`appsettings.json`** — base structure, checked into the repo with empty secrets.
- **`appsettings.Development.json`** — ready-to-use local development values (already pointing at the Dockerized PostgreSQL database described in [Running with Docker](#running-with-docker)).

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=appdb;Username=postgres;Password=postgres"
  },
  "Sso": {
    "Issuer": "kyc-trueface-dev",
    "Audience": "kyc-trueface-dev",
    "Key": "dev-only-signing-key-change-me-please-32chars",
    "ResetPasswordTokenExpiration": 3600
  },
  "App": {
    "CorsName": "DefaultCorsPolicy",
    "FrontendUrl": "http://localhost:5173"
  }
}
```

| Key | Description | Example |
|---|---|---|
| `ConnectionStrings:DefaultConnection` | PostgreSQL connection string | `Host=localhost;Port=5432;Database=appdb;Username=postgres;Password=postgres` |
| `Sso:Issuer` | JWT issuer | `kyc-trueface-dev` |
| `Sso:Audience` | JWT audience | `kyc-trueface-dev` |
| `Sso:Key` | JWT signing secret key | `your-256-bit-secret` |
| `Sso:ResetPasswordTokenExpiration` | Password reset token expiration, in seconds | `3600` |
| `App:CorsName` | CORS policy name | `DefaultCorsPolicy` |
| `App:FrontendUrl` | Frontend URL allowed by CORS | `http://localhost:5173` |

Any of these values can also be overridden via environment variables using ASP.NET Core's double-underscore convention (e.g. `ConnectionStrings__DefaultConnection`, `Sso__Key`) — this is the approach used when running the API container (see [Running with Docker](#running-with-docker)).

### Frontend

Create a `.env` file inside `frontend/webPartner/` based on the provided template:

```bash
cp frontend/webPartner/.example.env \
   frontend/webPartner/.env
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
cd backend

# Restore dependencies
dotnet restore

# Run the API (HTTPS on port 7065, HTTP on port 5184)
dotnet run --project KYC.TrueFace.Core.API/KYC.TrueFace.Core.API.csproj
```

The API will be available at:
- HTTPS: `https://localhost:7065`
- HTTP: `http://localhost:5184`

### Option 2 — Visual Studio

1. Open `backend/KYC.TrueFace.Core.sln` in Visual Studio 2022.
2. Set `KYC.TrueFace.Core.API` as the startup project.
3. Select the `https` launch profile.
4. Press **F5** to run with debugger or **Ctrl+F5** without.

---

## Running the Frontend

```bash
# Navigate to the frontend directory
cd frontend/webPartner

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

Make sure Docker is installed first — see the [official installation docs](https://docs.docker.com/get-docker/).

### Database (PostgreSQL via Docker Compose)

The local PostgreSQL instance is defined in `docker/database/docker-compose.yml`, together with a `pgAdmin` UI:

```bash
cd docker/database

docker compose --env-file env up -d
```

> The `--env-file env` flag is required because the file is named `env`, not `.env` — Docker Compose only auto-loads a file literally named `.env`.

This starts:
- **PostgreSQL 16** on `localhost:5432`
- **pgAdmin** on `http://localhost:5050` (login: `admin@local.dev` / `admin`)

Default credentials (from `docker/database/env`) are `postgres` / `postgres` with database `appdb` — these already match the values pre-configured in `appsettings.Development.json`.

To stop the containers:

```bash
docker compose --env-file env down
```

> Add `-v` to also remove the data volume and reset the database.

### Backend API

The backend includes a multi-stage `Dockerfile` for containerized deployments.

#### Build the image

```bash
# Run from inside backend/
cd backend

docker build -f KYC.TrueFace.Core.API/Dockerfile -t kyc-trueface-api .
```

#### Run the container

```bash
docker run -d \
  -p 8080:8080 \
  -e ConnectionStrings__DefaultConnection="Host=host.docker.internal;Port=5432;Database=appdb;Username=postgres;Password=postgres" \
  -e App__FrontendUrl="http://localhost:5173" \
  -e App__CorsName="DefaultCorsPolicy" \
  -e Sso__Key="your-256-bit-secret" \
  -e Sso__Issuer="KYC.TrueFace.Core.API" \
  -e Sso__Audience="KYC.TrueFace.Web.WebPartner" \
  --name kyc-trueface-api \
  kyc-trueface-api
```

The API will be available at `http://localhost:8080`.

> **Note:** Use `host.docker.internal` in `ConnectionStrings__DefaultConnection` to reach the PostgreSQL container (or any PostgreSQL instance running on your host machine) from inside the API container.

---

## Database Migrations

Migrations are managed by Entity Framework Core. Run all commands from the solution root.

> Before running any command below, make sure the PostgreSQL container is up (see [Database (PostgreSQL via Docker Compose)](#running-with-docker)) and that `ConnectionStrings:DefaultConnection` in `appsettings.Development.json` points to a reachable database.

```bash
cd backend

# Apply pending migrations to the database
dotnet ef database update \
  --project KYC.TrueFace.Core.Infra.Data/KYC.TrueFace.Core.Infra.Data.csproj \
  --startup-project KYC.TrueFace.Core.API/KYC.TrueFace.Core.API.csproj
```

---

## API Documentation

Swagger UI is available in development mode at:

```
https://localhost:7065/swagger
```

---

## License

MIT © 2026 Gustavo Do Espirito Santo
