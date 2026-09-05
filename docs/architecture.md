# Architecture

## Purpose

This is a deliberately small Clean Architecture reference, not a reusable application
framework. The Customer lifecycle is the executable example used to show where domain
rules, CQRS handlers, persistence and HTTP concerns belong.

~~~mermaid
flowchart LR
    Client[HTTP client] --> Api[ASP.NET Core API]
    Api --> Application[Application / CQRS]
    Application --> Domain[Domain model]
    Api --> Infrastructure[Infrastructure]
    Infrastructure --> Application
    Infrastructure --> Domain
    Infrastructure --> SQLite[(SQLite)]
~~~

## Layer responsibilities

| Layer | Owns | Does not own |
| --- | --- | --- |
| Domain | Customer invariants, Email value object | HTTP, database, MediatR |
| Application | Commands, queries, handlers, validation, repository port | EF Core and transport details |
| Infrastructure | EF Core context and repository adapter | Business decisions |
| API | Routing, composition root, Problem Details, health endpoint | Domain rules |

## Request flow

1. ASP.NET Core binds an HTTP request.
2. The controller creates a command or query and sends it through MediatR.
3. The validation behavior rejects malformed input before the handler executes.
4. The handler coordinates domain objects and the repository port.
5. EF Core persists to SQLite.
6. Known exceptions become consistent Problem Details responses.

## Deterministic rules

The application does not use an LLM at runtime. Customer names and email addresses are
validated deterministically. Email uniqueness is enforced by the use case and by a
database index, covering both normal execution and concurrent writes.

## Deliberate trade-offs

- **SQLite instead of a server database:** removes account and container prerequisites.
  A production service can replace the Infrastructure adapter without changing Domain.
- **EnsureCreated instead of migrations:** makes the starter run immediately. A derived
  production service should introduce reviewed migrations.
- **Controllers instead of minimal endpoints:** keeps the HTTP adapter familiar and easy
  to navigate in an architecture interview.
- **Single bounded context:** avoids inventing abstractions before a real second context
  exists.
- **No generic repository:** the use-case-specific port communicates intent and avoids a
  leaky persistence abstraction.
