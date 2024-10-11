# Use the official ASP.NET runtime image as a base image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Build stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy project files and restore dependencies
COPY MediscreenCMS/*.csproj ./MediscreenCMS/
COPY MediscreenCMS.Services/*.csproj ./MediscreenCMS.Services/
COPY MedisrceenCMS.Models/*.csproj ./MedisrceenCMS.Models/
RUN dotnet restore ./MediscreenCMS/MediscreenCMS.csproj

# Copy the rest of the code
COPY . .

# Publish the app to a specific output directory
WORKDIR /src/MediscreenCMS
RUN dotnet publish -c Release -o /app/publish

# Migration stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS migration
WORKDIR /src/MediscreenCMS
COPY --from=build /app/publish /app/publish
WORKDIR /app/publish

# Apply migrations here
RUN dotnet ef database update

# Final stage
FROM base AS final
WORKDIR /app

# Copy published output from build stage
COPY --from=build /app/publish .

# Set the entry point
ENTRYPOINT ["dotnet", "MediscreenCMS.dll"]
