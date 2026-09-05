# Agent guide

This repository is a small reference implementation for AI-assisted .NET development.
Agents must improve the codebase without turning the starter into a framework or a product.

## Architecture boundaries

- Domain owns entities, value objects and deterministic business rules. It must not reference MediatR, EF Core or ASP.NET Core.
- Application owns use cases, CQRS requests, validation and ports. It may reference only Domain.
- Infrastructure implements persistence and other adapters. It may reference Application and Domain.
- Api is the composition root and HTTP adapter. It must not contain business rules.
- Tests may reference any project.

Dependencies always point inward: Api -> Infrastructure/Application -> Domain.

## Implementation rules

- Prefer one complete vertical slice over placeholder abstractions.
- Keep commands responsible for state changes and queries read-only.
- Put invariants in the domain and orchestration in application handlers.
- Pass cancellation tokens through asynchronous calls.
- Return RFC 9457 Problem Details from HTTP errors.
- Never place secrets, model keys or personal data in source control.
- Do not introduce a cloud service when a local dependency is enough for the example.
- Explain non-obvious trade-offs in docs/architecture.md.

## AI-assisted workflow

For substantial changes, use the roles described in docs/ai-assisted-development.md.
The human remains responsible for scope and approval. AI output is a proposal until it
passes review, tests and the definition of done.

## Definition of done

Run these commands from the repository root:

~~~text
dotnet format CleanArchitecture.sln --verify-no-changes
dotnet build CleanArchitecture.sln --configuration Release --no-restore
dotnet test CleanArchitecture.sln --configuration Release --no-build
docker build --tag ai-assisted-dotnet-starter .
~~~

Update tests and documentation whenever behavior or an architectural decision changes.
