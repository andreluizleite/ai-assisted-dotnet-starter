---
name: .NET Architecture Rules
---

# Global Architecture

- Use .NET 8 with Clean Architecture and Domain-Driven Design (DDD).
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

# Folder Structure

## Domain
- Entities
- ValueObjects
- Aggregates
- Enums
- Services (domain services)
- Events (domain events)
- Interfaces (repository abstractions)

## Application
- Commands
- Queries
- Handlers
- DTOs
- Validators
- Behaviors (pipeline behaviors)
- Interfaces (application service abstractions)

## Infrastructure
- Persistence
- Repositories
- Configurations (EntityTypeConfiguration)
- Migrations
- ExternalServices (e.g., email, storage)

## Api
- Controllers
- Filters
- DTOs
- Middlewares
- Configuration

# CQRS + MediatR Rules

- Every write operation must be implemented as a Command + CommandHandler.
- Every read operation must be implemented as a Query + QueryHandler.
- Handlers must be small and orchestration-only.
- Handlers may not contain domain logic.
- Domain logic must be inside the Domain layer (entities, aggregates, domain services).
- Handlers must use repository interfaces (Domain layer), not implementations.

# Repository Rules

- Repository interfaces must be defined in Domain layer.
- Repository implementations must be placed in Infrastructure layer.
- DbContext lives in Infrastructure only.
- Entities must be configured using EF Core `IEntityTypeConfiguration<T>` classes.

# API Rules

- Use ASP.NET Core Minimal or Controllers (controllers preferred).
- Endpoints must return IActionResult or typed results.
- Validation should occur using FluentValidation.
- Controllers must never call Infrastructure directly.

# Domain Rules

- Entities must be Aggregate Roots when they manage invariants.
- All behavior must be placed inside entities or domain services.
- Entities must protect their invariants through constructors and methods.
- Value Objects must be immutable.
- Domain Events must be raised from Aggregate Roots when something meaningful happens.

# Coding Guidelines

- Namespaces must follow folder structure.
- Classes must be in English.
- Use dependency injection everywhere.
- Do not expose public setters in entities (use private setters).
- Use Guid as primary key for all aggregates.
