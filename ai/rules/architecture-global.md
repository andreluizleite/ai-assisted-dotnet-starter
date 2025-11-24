# Global Architecture

- Use .NET 10 with Clean Architecture and Domain-Driven Design (DDD).
- Solution must contain the following projects:
  - Domain
  - Application
  - Infrastructure
  - Api
- Layers must follow strict independence:
  - Domain has no dependency on any other layer.
  - Application depends only on Domain.
  - Infrastructure depends on Application and Domain.
  - Api depends only on Application.
- Apply CQRS using MediatR.
- Apply repository pattern.
- Use EF Core with SQL Server.
- Controllers must contain no business logic. They may only call the Application layer via MediatR.
- Business rules must be expressed in the Domain layer (entities, value objects, domain services, aggregates).
