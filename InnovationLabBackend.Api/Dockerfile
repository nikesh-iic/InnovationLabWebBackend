FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy entire solution so build system can resolve projects
COPY InnovationLabBackend.sln ./
COPY InnovationLabBackend.Api/ InnovationLabBackend.Api/

RUN dotnet restore "InnovationLabBackend.Api/InnovationLabBackend.Api.csproj"
RUN dotnet publish "InnovationLabBackend.Api/InnovationLabBackend.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "InnovationLabBackend.Api.dll"]
