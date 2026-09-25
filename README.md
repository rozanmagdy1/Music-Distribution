# Music Distribution

## Project Overview

Music Distribution is a track catalog and distribution application built with:

* ASP.NET Core Web API
* React + TypeScript
* SQL Server + Entity Framework Core
* JWT Authentication

The application manages artists, tracks, track statuses, and DSP distributions.

## Project Structure

```text
<repository-root>/
├── MusicDistribution_Backend/
│   └── src/
│       ├── MusicDistribution.API/
│       ├── MusicDistribution.Application/
│       ├── MusicDistribution.Domain/
│       └── MusicDistribution.Infrastructure/
└── MusicDistribution_Frontend/
    └── music-distribution/
```

## Prerequisites

* .NET 10 SDK
* Node.js `^20.19.0` or `>=22.12.0`
* SQL Server LocalDB (or another SQL Server instance)
* `dotnet-ef` 10.0.12

## Backend Setup

From the repository root:

```powershell
cd .\MusicDistribution_Backend
dotnet restore MusicDistribution_Backend.slnx
```

The default database uses SQL Server LocalDB:

```text
(localdb)\MSSQLLocalDB
```

The connection string can be changed in:

```text
src/MusicDistribution.API/appsettings.json
```

For development, set:

```powershell
$env:ASPNETCORE_ENVIRONMENT = "Development"
```

The development JWT configuration is stored in `appsettings.Development.json`. For a real environment, use a private signing key through environment variables or other secure configuration.

## Database Migrations

Install the EF Core CLI if needed:

```powershell
dotnet tool install --global dotnet-ef --version 10.0.12
```

Apply the existing migrations:

```powershell
dotnet ef database update `
  --project src/MusicDistribution.Infrastructure/MusicDistribution.Infrastructure.csproj `
  --startup-project src/MusicDistribution.API/MusicDistribution.API.csproj
```

The database is seeded with sample artists, DSPs, tracks, and distributions. User accounts are not seeded.

To create a new migration:

```powershell
dotnet ef migrations add <MigrationName> `
  --project src/MusicDistribution.Infrastructure/MusicDistribution.Infrastructure.csproj `
  --startup-project src/MusicDistribution.API/MusicDistribution.API.csproj `
  --output-dir Migrations
```

## Run the Backend

```powershell
dotnet run --project src/MusicDistribution.API/MusicDistribution.API.csproj --launch-profile https
```

The API runs at:

```text
https://localhost:7174
```

Swagger:

```text
https://localhost:7174/swagger
```

If needed, trust the development HTTPS certificate:

```powershell
dotnet dev-certs https --trust
```

## Run the Frontend

Open another terminal:

```powershell
cd .\MusicDistribution_Frontend\music-distribution
npm install
npm run dev
```

The frontend runs at:

```text
http://localhost:5173
```

If the backend is running on the HTTPS profile above, create:

```text
.env.local
```

with:

```dotenv
VITE_API_BASE_URL=https://localhost:7174/api
```

Restart Vite after changing the environment file.

## How to Obtain a JWT Token

There are no seeded user accounts. Register a new account first:

```http
POST /api/auth/register
Content-Type: application/json
```

```json
{
  "email": "you@example.com",
  "password": "your-password"
}
```

Then log in:

```http
POST /api/auth/login
Content-Type: application/json
```

```json
{
  "email": "you@example.com",
  "password": "your-password"
}
```

The response contains an `accessToken`.

Use it for protected endpoints:

```http
Authorization: Bearer <JWT_TOKEN>
```

Swagger also provides an **Authorize** button for entering the token.

## API Overview

| Area           | Method | Route                            | Auth |
| -------------- | ------ | -------------------------------- | ---- |
| Authentication | POST   | `/api/auth/register`             | No   |
| Authentication | POST   | `/api/auth/login`                | No   |
| Artists        | GET    | `/api/artists`                   | No   |
| Artists        | POST   | `/api/artists`                   | No   |
| Tracks         | GET    | `/api/tracks`                    | No   |
| Tracks         | GET    | `/api/tracks/{id}`               | No   |
| Tracks         | POST   | `/api/tracks`                    | No   |
| Tracks         | PATCH  | `/api/tracks/{id}/status`        | Yes  |
| Distribution   | GET    | `/api/tracks/{id}/distributions` | No   |
| Distribution   | POST   | `/api/tracks/{id}/distribute`    | Yes  |
| DSPs           | GET    | `/api/dsps`                      | No   |

`GET /api/tracks` supports filtering by `artistId`, `genre`, and `status`.

## Troubleshooting

**Database error:**
Make sure SQL Server LocalDB is available and the connection string is correct. Run the migrations again if necessary.

**CORS error:**
Make sure the frontend is running on `http://localhost:5173`.

**Frontend cannot connect to the API:**
Check `VITE_API_BASE_URL` in `.env.local` and restart Vite.

**JWT error:**
Make sure the JWT configuration has a valid signing key, issuer, audience, and expiration.

**HTTPS certificate warning:**

```powershell
dotnet dev-certs https --trust
```
