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

- [Docker and Docker Compose](https://docs.docker.com/get-started/get-docker/) (for Docker setup)
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0) (for non-Docker setup)
- [PostgreSQL](https://www.postgresql.org/) database (for non-Docker setup)
- (Optional) [Visual Studio Code](https://code.visualstudio.com/) or Visual Studio

## Getting Started

1. **Clone the repository**

   ```bash
   git clone https://github.com/your-org/InnovationLabWebBackend.git
   cd InnovationLabWebBackend
   ```

2. **Configure the credentials**

   - Create `appsettings.Development.json` file alongside the `Program.cs` file inside of `InnovationLabBackend.Api` folder.
   - Add the database connection string for PostgreSQL in `appsettings.Development.json` file with the name `DbConnection`.
   - Add the JWT credentials for the issuer, audience, and key in the `appsettings.Development.json` file with the name `Issuer`, `Audience`, and `Key` respectively.
     The `appsettings.Development.json` file should look something like this after all the configurations

   ```appsettings.Development.json
    {
      "ConnectionStrings": {
         "DbConnection": "Host=<HOST_NAME>:<PORT>;Database=<DB_NAME>;Username=<POSTGRES_USER>;Password=<POSTGRES_PASSWORD>;"
      },
      "Jwt": {
         "Issuer": "<JWT_ISSUER>",
         "Audience": "<JWT_AUDIENCE>",
         "Key": "<256_BIT_JWT_KEY>"
      },
      "Cloudinary": {
         "Url": "cloudinary://<API_KEY>:<API_SECRET>@<CLOUD_NAME>"
      },
      "Logging": {
         "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
         }
      }
    }
   ```

   **FOR DOCKER ONLY**

   - Create `.env` file in the root of the directory to store the db credentials for docker
   - Add the database username, password, and database name for the postgres db in docker
   - The `.env` file should look like this after all the configuration

   ```.env
   POSTGRES_USER=<POSTGRES_USER>
   POSTGRES_PASSWORD=<POSTGRES_PASSWORD>
   POSTGRES_DB=<DB_NAME>
   ```

   **Note**:

   - Replace `HOST_NAME`, `PORT`, `DB_NAME`, `POSTGRES_USER`, and `POSTGRES_PASSWORD` with your host, port, database name, postgres username, and postgress password to configure the PostgreSQL database.
     - The defaults are `localhost`, `5432`, `InnovationLabDb`, `postgres`, and `password`
     - _FOR DOCKER_ the default host is `db` instead of `localhost`
   - Replace `JWT_ISSUER`, `JWT_AUDIENCE`, and `256_BIT_JWT_KEY` with your JWT issuer, JWT audience, and your 256 bit JWT secret key
   - Replace `API_KEY`, `API_SECRET`, and `CLOUD_NAME` with your cloudinary api key, api secret, and the cloud name provided from the Cloudinary dashboard

3. **Run the Application**

   **Using Docker Compose (RECOMMENDED)**

   - Start the API and PostgreSQL database using Docker Compose:

   ```bash
   docker compose up --build
   ```

   - This builds docker image from `InnovationLabBackend.Api` and starts the services defined in `docker-compose.yml`.
   - The API will be available at `http://localhost:8080`.

   **Without Using Docker Compose**

   ```bash
   dotnet run
   ```

   Alternatively, use the `Debug and Run` option from the GUI in VS Code or Visual Studio

4. **Access the API documentation**

   **Using Docker Compose**

   - Open your browser and go to: http://localhost:8080/swagger (or the URL shown in docker)

   **Without Using Docker Compose**

   - Open your browser and go to: https://localhost:5217/swagger (or the URL shown in the terminal)

5. **Stop the Application**
   - Press `Ctrl+C` to stop the docker containers, or run:
   ```bash
   docker compose down
   ```

## Usage

- Use the Swagger UI to test API endpoints for authentication and testimonials.
- Register a new user, then log in and use the JWT token for authorized endpoints.

## Notes

- Default environment is Development.
- Do not commit sensitive data or secrets to the repository.
- For contribution guidelines, see CONTRIBUTING.md.
