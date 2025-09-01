# Lwu.CourseManagement.Api
====================

A modular ASP.NET Core project for managing course-related data, structured using Clean Architecture principles and powered by Entity Framework Core for database operations.

---

Project Structure
-----------------

Lwu.CourseManagement
├── Lwu.CourseManagement.Api              # API Layer (Startup Project)
├── Lwu.CourseManagement.Application      # Application Layer (Use Cases, DTOs)
├── Lwu.CourseManagement.Domain           # Domain Layer (Entities)
└── Lwu.CourseManagement.Infrastructure   # Infrastructure Layer (EF Core, DbContext, Migrations)

---

Getting Started
---------------

Prerequisites:
- .NET 9.0 or later (https://dotnet.microsoft.com/)
- PGSQL Server (or another configured database provider)
- EF Core CLI tools

Install EF Core tools (if not already installed):

    dotnet tool install --global dotnet-ef

---

Build & Run
-----------

Restore & Build:

    dotnet restore
    dotnet build

Run the API:

    dotnet run --project Lwu.CourseManagement.Api

---

Entity Framework Core – Migrations
----------------------------------

This project uses EF Core migrations to manage database schema changes.

Add Initial Migration:

    dotnet ef migrations add InitialCreate --project Lwu.CourseManagement.Infrastructure --startup-project Lwu.CourseManagement.Api

Applies the initial migration from the current model snapshot.

Apply Migration to Database:

    dotnet ef database update --project Lwu.CourseManagement.Infrastructure --startup-project Lwu.CourseManagement.Api

Applies the latest migration(s) to the connected database.

Fix Seed Data:

    dotnet ef migrations add FixedSeedData --project Lwu.CourseManagement.Infrastructure --startup-project Lwu.CourseManagement.Api

Use when modifying or correcting seed data logic.

Sync EF Model with Database:

    dotnet ef migrations add SyncModel --project Lwu.CourseManagement.Infrastructure --startup-project Lwu.CourseManagement.Api

Use after making model or configuration changes that impact schema.

---
