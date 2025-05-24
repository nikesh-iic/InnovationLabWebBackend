FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG CONFIGURATION=Release
WORKDIR /src

COPY InnovationLabBackend.sln ./
COPY InnovationLabBackend.Api/ InnovationLabBackend.Api/

RUN dotnet restore "InnovationLabBackend.Api/InnovationLabBackend.Api.csproj"
RUN dotnet publish "InnovationLabBackend.Api/InnovationLabBackend.Api.csproj" -c $CONFIGURATION -o /app/publish

FROM base AS final
ARG CONFIGURATION=Release
WORKDIR /app

COPY --from=build /app/publish .

RUN if [ "$CONFIGURATION" = "Debug" ]; then \
    cp /src/InnovationLabBackend.Api/appsettings.Development.json . ; \
    fi

ENTRYPOINT ["dotnet", "InnovationLabBackend.Api.dll"]
