# InnovationLab Backend

A simple ASP.NET Core Web API backend for for the Innovation Lab project.

## Features

- User registration and login with JWT authentication
- Two-factor authentication (2FA) support
- CRUD operations for testimonials
- Role-based authorization
- Swagger API documentation

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/) database
- (Optional) [Visual Studio Code](https://code.visualstudio.com/) or Visual Studio

## Getting Started

1. **Clone the repository**

   ```bash
   git clone https://github.com/your-org/InnovationLabWebBackend.git
   cd InnovationLabWebBackend
   ```

2. **Configure the credentials**

   - Create `appsettings.Development.json` file alongside the `appsettings.json` file inside of `InnovationLabBackend.Api` folder.
   - Add the database connection string for PostgreSQL in `appsettings.Development.json` file with the name `DbConnection`.
   - Add the JWT credentials for the issuer, audience, and key in the `appsettings.Development.json` file with the name `Issuer`, `Audience`, and `Key` respectively.
     The `appsettings.Development.json` file should look something like this after all the configurations

   ```appsettings.Development.json
    {
        "ConnectionStrings": {
            "DbConnection": "Host=HOST_NAME:PORT;Database=DB_NAME;Username=DB_USERNAME;Password=DB_PASSWORD;"
        },
       "Jwt": {
         "Issuer": "JWT_ISSUER",
         "Audience": "JWT_AUDIENCE",
         "Key": "YOUR_SECRET_JWT_KEY"
       },
       "Logging": {
         "LogLevel": {
           "Default": "Information",
           "Microsoft.AspNetCore": "Warning"
           }
        }
        *** other config if any ***
    }
   ```

3. **Start the API server**

   ```bash
   dotnet run
   ```

   Alternatively, use the `Debug and Run` option from the GUI in VS Code or Visual Studio

4. **Access the API documentation**
    - Open your browser and go to: https://localhost:7293/swagger (or the URL shown in the terminal)

## Usage

- Use the Swagger UI to test API endpoints for authentication and testimonials.
- Register a new user, then log in and use the JWT token for authorized endpoints.

## Notes

- Default environment is Development.
- Do not commit sensitive data or secrets to the repository.
- For contribution guidelines, see CONTRIBUTING.md.
