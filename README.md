# KYC TrueFace

A full-stack KYC (Know Your Customer) platform for identity verification, built with ASP.NET Core 8 and React 19.

## Table of Contents

- [Tech Stack](#tech-stack)
- [Environment Variables](#environment-variables)
- [Running the Backend](#running-the-backend)
- [Running the Frontend](#running-the-frontend)
- [Running with Docker](#running-with-docker)
- [Production Deployment (Backend)](#production-deployment-backend)
- [Database Migrations](#database-migrations)
- [Onboarding Face Validation](#onboarding-face-validation)
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

#### Face validation (AWS Rekognition)

The onboarding endpoint stores both images on the VPS and a background service compares them
with [Amazon Rekognition](https://docs.aws.amazon.com/rekognition/latest/APIReference/API_CompareFaces.html).
Both sections live in `appsettings.json`:

```json
{
  "Onboarding": {
    "StoragePath": "/var/kyc-trueface/onboarding",
    "PollingIntervalSeconds": 5,
    "BatchSize": 10,
    "StaleProcessingMinutes": 5,
    "MaxAttempts": 3,
    "MaxImageSizeBytes": 5242880,
    "AllowedContentTypes": [ "image/jpeg", "image/png" ]
  },
  "Aws": {
    "Region": "us-east-1",
    "AccessKeyId": "",
    "SecretAccessKey": "",
    "ServiceUrl": "",
    "Rekognition": {
      "AutoApproveThreshold": 90,
      "ManualReviewThreshold": 30,
      "TimeoutSeconds": 30,
      "MaxErrorRetry": 2
    }
  }
}
```

| Key | Description | Default |
|---|---|---|
| `Onboarding:StoragePath` | Folder where the uploaded images are written. Only the path *relative* to it is stored in the database | `/var/kyc-trueface/onboarding` |
| `Onboarding:PollingIntervalSeconds` | How often the background service looks for pending records | `5` |
| `Onboarding:BatchSize` | Maximum records claimed per tick | `10` |
| `Onboarding:StaleProcessingMinutes` | After this long, a record stuck in `Processing` is claimed again (e.g. the API restarted mid-comparison) | `5` |
| `Onboarding:MaxAttempts` | Attempts on transient failures before the record is sent to manual review | `3` |
| `Onboarding:MaxImageSizeBytes` | Per-image upload limit | `5242880` (5 MB) |
| `Onboarding:AllowedContentTypes` | Accepted upload content types | `image/jpeg`, `image/png` |
| `Aws:Region` | Region used for Rekognition | `us-east-1` |
| `Aws:AccessKeyId` / `Aws:SecretAccessKey` | Static credentials. **Leave both empty to use the default AWS credential chain** (environment variables, shared profile, instance role) | empty |
| `Aws:ServiceUrl` | Endpoint override for a local stub or LocalStack. Empty means the real AWS endpoint | empty |
| `Aws:Rekognition:AutoApproveThreshold` | Similarity (0-100) from which the onboarding is approved automatically | `90` |
| `Aws:Rekognition:ManualReviewThreshold` | Similarity (0-100) below which the onboarding is denied automatically. Between this value and the auto-approve threshold the onboarding is held for manual review | `30` |
| `Aws:Rekognition:TimeoutSeconds` | HTTP timeout for the Rekognition call | `30` |
| `Aws:Rekognition:MaxErrorRetry` | Retries performed by the AWS SDK itself | `2` |

> The IAM user (or role) only needs the `rekognition:CompareFaces` permission.

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

## Production Deployment (Backend)

The backend deploys automatically to a VPS via [.github/workflows/deploy-backend.yml](.github/workflows/deploy-backend.yml) on every push to `main` that touches `backend/**`. The workflow:

1. Builds the API Docker image and pushes it to GitHub Container Registry (`ghcr.io/<owner>/kyc-trueface-api`), tagged with `latest` and the commit SHA.
2. Copies [deploy/docker-compose.prod.yml](deploy/docker-compose.prod.yml) to the VPS.
3. SSHes into the VPS, writes a `.env` file from GitHub secrets, pulls the new image and runs `docker compose up -d`.

Database migrations are applied automatically on API startup (see `Program.cs`), so no manual migration step is needed after a deploy. The Postgres container only binds to `127.0.0.1` on the VPS (not reachable from the internet); use an SSH tunnel if you ever need to inspect it manually (see [Inspecting production migrations manually](#inspecting-production-migrations-manually) below).

### One-time VPS setup

Run once on the VPS (as root — e.g. via the provider's browser terminal):

```bash
# Install Docker Engine + Compose plugin
curl -fsSL https://get.docker.com | sh

# Create a dedicated, non-root deploy user
adduser --disabled-password --gecos "" deploy
usermod -aG docker deploy
mkdir -p /opt/kyc-trueface
chown deploy:deploy /opt/kyc-trueface

# Generate an SSH key pair for GitHub Actions and authorize it for "deploy"
mkdir -p /home/deploy/.ssh
ssh-keygen -t ed25519 -f /home/deploy/.ssh/gh_actions -N "" -C "github-actions"
cat /home/deploy/.ssh/gh_actions.pub >> /home/deploy/.ssh/authorized_keys
chown -R deploy:deploy /home/deploy/.ssh
chmod 700 /home/deploy/.ssh
chmod 600 /home/deploy/.ssh/authorized_keys /home/deploy/.ssh/gh_actions

cat /home/deploy/.ssh/gh_actions   # copy this into the VPS_SSH_KEY secret below, then remove it from disk
```

### GitHub repository secrets

Configure these under **Settings → Secrets and variables → Actions** in the GitHub repo:

| Secret | Description |
|---|---|
| `VPS_HOST` | VPS public IP address |
| `VPS_PORT` | SSH port (usually `22`) |
| `VPS_USER` | `deploy` |
| `VPS_SSH_KEY` | Private key generated above (`gh_actions`) |
| `GHCR_PAT` | GitHub PAT (classic) with `read:packages` scope, used by the VPS to pull the image from GHCR |
| `POSTGRES_PASSWORD` | Password for the production Postgres container |
| `SSO_KEY` | Random 32+ character JWT signing secret |
| `APP_FRONTEND_URL` | Production frontend URL (used for CORS) |
| `AWS_REGION` | AWS region for Rekognition (e.g. `us-east-1`) |
| `AWS_ACCESS_KEY_ID` | Access key of an IAM user allowed to call `rekognition:CompareFaces` |
| `AWS_SECRET_ACCESS_KEY` | Secret key for the IAM user above |
| `AWS_REKOGNITION_AUTO_APPROVE_THRESHOLD` | Optional. Similarity from which the onboarding is approved automatically (defaults to `90`) |
| `AWS_REKOGNITION_MANUAL_REVIEW_THRESHOLD` | Optional. Similarity below which the onboarding is denied automatically; between the two thresholds it goes to manual review (defaults to `30`) |

> To add more settings later (`Smtp__*`, `PasswordHashing__Pepper`, etc.), extend the `.env` heredoc in the workflow's deploy step and the corresponding service in `deploy/docker-compose.prod.yml`.

### Custom domain + HTTPS

The API is fronted by [Caddy](https://caddyserver.com/) (see [deploy/Caddyfile](deploy/Caddyfile) and the `caddy` service in [deploy/docker-compose.prod.yml](deploy/docker-compose.prod.yml)), which automatically obtains and renews a Let's Encrypt certificate for `api.kyc-trueface.com.br` and reverse-proxies to the `api` container. The API container no longer publishes port 8080 to the internet — Caddy (ports 80/443) is the only public entry point.

To point a subdomain at the VPS:

1. In the DNS zone for `kyc-trueface.com.br` (registro.br, or wherever its nameservers are managed), add an **A record**: `api` → `<VPS_HOST>`.
2. On the VPS, make sure ports 80 and 443 are reachable (if `ufw` is active: `ufw allow 80/tcp && ufw allow 443/tcp`).
3. DNS must already be resolving before the first deploy — Caddy requests the certificate on startup and needs to reach the domain over port 80 (ACME HTTP-01 challenge).
4. Push (or re-run the `Deploy Backend` workflow) — it recreates the `caddy` container with the current `Caddyfile`.
5. Update `VITE_URL_API_BASE` in the Vercel project's environment variables to `https://api.kyc-trueface.com.br/api`, then redeploy the frontend (Vercel env vars are baked in at build time).

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

### Inspecting production migrations manually

Migrations run automatically on API startup, so this is only needed to inspect the schema, run one-off queries, or apply a migration by hand if auto-migration is ever disabled. The production Postgres container only binds to `127.0.0.1:5432` on the VPS (not exposed to the internet), so it's reached by opening an SSH tunnel and connecting through it.

> Use your **own** personal SSH key for this — not the `gh_actions` key from [One-time VPS setup](#one-time-vps-setup), which is dedicated to GitHub Actions and should never leave the VPS. If you don't have a key yet, generate one locally (`ssh-keygen -t ed25519`) and append its `.pub` to `/home/deploy/.ssh/authorized_keys` on the VPS.

```bash
# In one terminal: open a tunnel to the VPS's Postgres (uses your own personal key, authorized on the "deploy" user)
ssh -N -L 5432:127.0.0.1:5432 deploy@<VPS_HOST> -i /path/to/your_own_key

# In another terminal, from backend/, apply migrations through the tunnel
cd backend

dotnet ef database update \
  --project KYC.TrueFace.Core.Infra.Data/KYC.TrueFace.Core.Infra.Data.csproj \
  --startup-project KYC.TrueFace.Core.API/KYC.TrueFace.Core.API.csproj \
  --connection "Host=127.0.0.1;Port=5432;Database=appdb;Username=postgres;Password=<POSTGRES_PASSWORD secret>"
```

> Not required in the normal flow — the API applies pending migrations itself on startup.

---

## Onboarding Face Validation

An onboarding is a pair of images — a photo of the identity document and a selfie — that the
platform compares automatically. Uploading is synchronous; the comparison is not.

```
POST /api/v1/onboarding        ──▶  images written under Onboarding:StoragePath
                                    row inserted in "Onboardings" with Situation = Pending
                                    202 Accepted { code, situation }

OnboardingWorker (every 5s)    ──▶  claims pending rows  (Situation → Processing)
                                    CompareFaces on AWS Rekognition
                                    ├─ similarity >= threshold  → Approved
                                    ├─ similarity <  threshold  → Denied
                                    └─ error / no face found    → ManualReview
```

A record sitting in `ManualReview` is settled by a person: they look at both images, write an
observation and approve or deny it. That decision moves the record to `Approved`/`Denied` and is
kept in `OnboardingsResults` together with the reviewer's user code.

`Situation` values are `Pending = 1`, `Approved = 2`, `Denied = 3`, `Processing = 4`,
`ManualReview = 5`. Every transition also records `SituationDt`, the observed `Similarity` and a
human-readable `SituationMessage`.

A transient failure (network, throttling) puts the record back to `Pending` and it is retried on
the next tick, up to `Onboarding:MaxAttempts`; after that it goes to `ManualReview`. Errors that a
retry cannot fix — no face detected, unreadable image, missing file — go straight to
`ManualReview`. If the API stops mid-comparison, the row stays in `Processing` and is claimed
again after `Onboarding:StaleProcessingMinutes`.

### Endpoints

| Method | Route | Roles | Description |
|---|---|---|---|
| `POST` | `/api/v1/onboarding` | `ADMINISTRATOR`, `MASTER` | `multipart/form-data` with `name`, `idNumber`, and the `document` and `selfie` files. Returns `202` with the new code and situation |
| `GET` | `/api/v1/onboarding/manual-review` | all | Records waiting for a human decision |
| `GET` | `/api/v1/onboarding/reviewed` | all | Records already approved or denied. Accepts `?situation=Approved` or `?situation=Denied` to narrow it |
| `GET` | `/api/v1/onboarding/{code}/image/{kind}` | all | Serves one of the stored images. `kind` is `document` or `selfie` |
| `POST` | `/api/v1/onboarding/{code}/review` | `ADMINISTRATOR`, `MASTER` | `{ "approved": true, "observation": "..." }`. Only valid while the record is in `ManualReview` |
| `GET` | `/api/v1/dashboard/summary` | all | Counters for the dashboard cards: onboardings received, denied and approved in the last 7 days, records waiting for a human decision, and the ones a reviewer approved or denied in the last 30 days |

All of them are scoped to the partner in the caller's JWT — another partner's record answers as if
it did not exist.

```bash
curl -X POST https://api.kyc-trueface.com.br/api/v1/onboarding   -H "Authorization: Bearer <token>"   -F "name=Joana Pereira"   -F "idNumber=529.982.247-25"   -F "document=@document.jpg;type=image/jpeg"   -F "selfie=@selfie.jpg;type=image/jpeg"
```

### Web app

Two screens in `frontend/webPartner` cover the flow:

| Route | Screen | What it does |
|---|---|---|
| `/onboarding` | Manual review queue | Lists what the automatic check could not settle, with the reason. The **+** button opens the upload form (name, CPF, document photo, selfie); the eye icon opens both images; the check icon approves or denies with an observation |
| `/history/onboarding` | History | Lists approved and denied records, filterable by situation, and opens the outcome — similarity for automatic decisions, the reviewer's observation for manual ones |

Both screens filter by name or CPF client-side. Users with the `COMMUN` role see the lists and the
images but cannot upload or decide, matching the endpoint roles above.

> In production the images live in the `onboarding_images` Docker volume mounted at
> `/var/kyc-trueface/onboarding`, so they survive container recreation. The database stores only
> the path relative to that root (`<partner>/<year>/<month>/<code>-document.jpg`), so the root can
> be remounted elsewhere without a data migration.

---

## API Documentation

Swagger UI is available in development mode at:

```
https://localhost:7065/swagger
```

---

## License

MIT © 2026 Gustavo Do Espirito Santo
