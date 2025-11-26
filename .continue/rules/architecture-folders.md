# Folder Structure
- src

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

## Worker
- Queue
- Handlers
- Configuration
- Extensions

## Api
- Controllers
- Filters
- DTOs
- Middlewares
- Configuration
