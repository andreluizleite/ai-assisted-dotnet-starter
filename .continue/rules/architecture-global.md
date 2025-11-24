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

# Notifications

- The Application layer publishes integration events to an Outbox table.
- The Infrastructure layer contains an INotificationPublisher implementation.
- In local development, INotificationPublisher must be a fake/mock publisher that only logs or stores messages in memory.
- In the future, this interface will be implemented using AWS SNS/SQS, but the Domain and Application layers must not reference AWS directly.
- Notification publishing must only occur in the Infrastructure layer, never in Domain or Application logic.

