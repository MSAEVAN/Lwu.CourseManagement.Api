# Base image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 8080

# Build image
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /build

# Copy csproj files (note: Dockerfile is already inside Lwu.CourseManagement.Api)
COPY Lwu.CourseManagement.Infrastructure/Lwu.CourseManagement.Infrastructure.csproj ./Lwu.CourseManagement.Infrastructure/
COPY Lwu.CourseManagement.Domain/Lwu.CourseManagement.Domain.csproj ./Lwu.CourseManagement.Domain/
COPY Lwu.CourseManagement.Application/Lwu.CourseManagement.Application.csproj ./Lwu.CourseManagement.Application/
COPY Lwu.CourseManagement.Api/Lwu.CourseManagement.Api.csproj ./Lwu.CourseManagement.Api/

# Restore dependencies
RUN dotnet restore Lwu.CourseManagement.Api/Lwu.CourseManagement.Api.csproj

# Copy everything
COPY .. .

WORKDIR /build/Lwu.CourseManagement.Api
RUN dotnet build Lwu.CourseManagement.Api.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish Lwu.CourseManagement.Api.csproj -c Release -o /app/publish

# Final image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Lwu.CourseManagement.Api.dll"]
