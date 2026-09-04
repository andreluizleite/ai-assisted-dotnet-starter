# Agentic .NET Architecture Starter

[![CI](https://github.com/andreluizleite/Andre-Architecture/actions/workflows/ci.yml/badge.svg)](https://github.com/andreluizleite/Andre-Architecture/actions/workflows/ci.yml)
[![.NET 10](https://img.shields.io/badge/.NET-10-512BD4)](https://dotnet.microsoft.com/)

A small, executable reference for building .NET services with Clean Architecture,
DDD-oriented domain rules, CQRS and a controlled AI-assisted engineering workflow.

This is intentionally a starter, not a framework. It demonstrates one complete Customer
vertical slice rather than many empty abstractions.

## Why this repository exists

AI coding tools are most useful when architecture constraints, acceptance criteria and
verification steps are explicit. This repository combines:

- durable instructions for coding agents in AGENTS.md;
- a practical multi-role workflow for analysis, implementation, review and testing;
- deterministic C# business rules that AI is not allowed to replace;
- an executable ASP.NET Core example with automated quality gates.

No API key, cloud account or paid resource is required.

## Technology

- .NET 10 and ASP.NET Core
- Clean Architecture and CQRS with MediatR
- Domain entity and Email value object
- FluentValidation through a MediatR pipeline
- EF Core with SQLite
- RFC 9457 Problem Details
- Native OpenAPI and health checks
- xUnit unit and API integration tests
- Docker, GitHub Actions and Dependabot

## Architecture

~~~text
HTTP -> API -> Application -> Domain
          |          ^
          v          |
     Infrastructure -+
          |
        SQLite
~~~

The dependency direction points inward. Domain contains no ASP.NET Core, EF Core or
MediatR references. See [the architecture guide](docs/architecture.md) for responsibilities
and deliberate trade-offs.

## Run locally

Requirements: .NET SDK 10.0.400 or a compatible patch release.

~~~powershell
dotnet restore CleanArchitecture.sln
dotnet run --project src/Api/CleanArchitecture.Api.csproj
~~~

Then open:

- API summary: http://localhost:5277/
- Customers: http://localhost:5277/api/v1/customers
- Health: http://localhost:5277/health
- OpenAPI: http://localhost:5277/openapi/v1.json

The SQLite database is created automatically and seeded with two fictional customers.

Create a customer from PowerShell:

~~~powershell
$body = @{
    firstName = "Grace"
    lastName = "Hopper"
    email = "grace@example.com"
} | ConvertTo-Json

Invoke-RestMethod -Method Post -Uri "http://localhost:5277/api/v1/customers" -ContentType "application/json" -Body $body
~~~

The file src/Api/CleanArchitecture.Api.http contains additional requests for Visual Studio
and compatible editors.

## Run with Docker

~~~powershell
docker compose up --build
~~~

The API is available at http://localhost:8080 and data is stored in a named Docker volume.

## Verify the repository

~~~powershell
dotnet format CleanArchitecture.sln --verify-no-changes
dotnet build CleanArchitecture.sln --configuration Release --no-restore
dotnet test CleanArchitecture.sln --configuration Release --no-build
~~~

The tests cover domain invariants, email normalization, validation, duplicate email
handling, Problem Details, persistence and the full create-update-delete lifecycle.

## AI-assisted development

Read [the agent playbook](docs/ai-assisted-development.md) before extending the starter.
It defines:

- when one agent is enough and when independent roles add value;
- what must remain deterministic;
- the human approval boundary;
- a reusable change template;
- the evidence required before a change is considered done.

The application itself does not call an LLM. Runtime AI belongs in a product with a real
use case, while this repository focuses on disciplined AI-assisted software development.

## Example business behavior

- Customer names are required, trimmed and limited to 100 characters.
- Email addresses are normalized to lowercase.
- An email can belong to only one customer.
- Commands mutate state; queries remain read-only.
- Known failures return consistent Problem Details with a trace identifier.

## Suggested interview summary

> I created a .NET architecture starter that demonstrates how to use coding agents within
> engineering guardrails. The repository provides durable agent context, Clean Architecture
> boundaries, a complete CQRS vertical slice, deterministic rules, integration tests and CI.
> The goal is not code generation at any cost; it is traceable, reviewable and repeatable delivery.

## Scope

This repository is suitable as a learning reference or the starting point for a small
service. Authentication, distributed messaging, observability backends and cloud
infrastructure are intentionally excluded until a derived product has those requirements.
