---
name: .NET Architecture Rules
---

# Global Architecture

- Use .NET 8 with Clean Architecture and DDD.
- Layers:
  - `Domain`
  - `Application`
  - `Infrastructure`
  - `Api`
- Use CQRS with MediatR.
- Use EF Core with SQL Server.
- Do not put business rules in controllers.
- Controllers call Application layer only.
- Application layer coordinates use cases.
- Domain layer holds entities, value objects, aggregates and domain services.
- Infrastructure layer implements repositories, DbContext and external integrations.

# Folder Structure

- `src/Domain`:
  - `Entities`, `ValueObjects`, `Aggregates`, `Enums`, `Services`, `Events`
- `src/Application`:
  - `Commands`, `Queries`, `Handlers`, `Dtos`, `Validators`
- `src/Infrastructure`:
  - `Persistence`, `Repositories`, `Configurations`, `Migrations`
- `src/Api`:
  - `Controllers`, `Filters`, `Dtos`, `Configurations`

# CQRS + MediatR

- Every write operation must be a `Command` + `CommandHandler`.
- Every read operation must be a `Query` + `QueryHandler`.
- Handlers must be in the Application layer.
- Handlers should be small and delegate domain logic to aggregates or domain services.
