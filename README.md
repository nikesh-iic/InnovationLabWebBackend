# InnovationLab Backend

A simple ASP.NET Core Web API backend for for the Innovation Lab project.

## Features

- User registration and login with JWT authentication
- Two-factor authentication (2FA) support
- CRUD operations for testimonials
- Role-based authorization
- Swagger API documentation
- Cloudinary integration for file upload (E.g. images and videos)

## Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (for non-Docker setup)
- [Docker and Docker Compose](https://docs.docker.com/get-started/get-docker/) (for Docker setup)
- [PostgreSQL](https://www.postgresql.org/) database (for non-Docker setup)
- (Optional) [Visual Studio Code](https://code.visualstudio.com/) or Visual Studio

## Getting Started

### Run with Docker compose (Recommended)

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

   - Create a `.env` file in the `InnovationLabBackend.Api` directory to store sensitive credentials

   ```
   CLOUDINARY_URL=cloudinary://<API_KEY>:<API_SECRET>@<CLOUD_NAME>
   # PostgreSQL credentials for the database container
   POSTGRES_USER=postgres
   POSTGRES_PASSWORD=password
   POSTGRES_DB=InnovationLabDb
   ```

   **Note**:

   - Replace `CLOUDINARY_URL` value with your Cloudinary URL from the Cloudinary dashboard
   - Use `POSTGRES_USER`, `POSTGRES_PASSWORD`, and `POSTGRES_DB` to configure the PostgreSQL database. The defaults (`postgres`, `password`, `InnovationLabDb`) match the docker-compose.yml setup.

3. **Run the Application**

   - Start the API and PostgreSQL database using Docker Compose:

   ```bash
   docker-compose up --build
   ```

   - This builds docker image from `InnovationLabBackend.Api` and starts the services defined in `docker-compose.yml`.
   - The API will be available at `http://localhost:8080`.

4. **Access the API documentation**

   - Open your browser and go to: http://localhost:8080/swagger (or the URL shown in the terminal)

5. **Stop the Application**
   - Press `Ctrl+C` to stop the containers, or run:
   ```bash
   docker-compose down
   ```

### Run without Docker

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

   - Create a `.env` file in the `InnovationLabBackend.Api` directory to store sensitive credentials

   ```
   CLOUDINARY_URL=cloudinary://<API_KEY>:<API_SECRET>@<CLOUD_NAME>
   ```

3. **Run the Application**

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
