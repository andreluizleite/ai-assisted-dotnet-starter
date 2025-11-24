# Andre-Architecture

This repository contains the initial structure and configuration required to enable AI-assisted generation of a .NET 8 backend using Clean Architecture, Domain-Driven Design (DDD), and CQRS.

## Project Setup Summary

### 1. Base Project Structure
The repository includes a foundational layout intended to support the generation of:
- Domain layer (entities, value objects, aggregates, domain services)
- Application layer (commands, queries, handlers, DTOs)
- Infrastructure layer (repository implementations, EF Core, database configuration)
- API layer (controllers, endpoints, request/response models)

This structure prepares the repository for automated code creation aligned with modern .NET architectural standards.

### 2. Continue.dev Integration
A `.continue` directory was added to the project. It contains:
- rules: architectural and coding guidelines
- contexts: project-specific contextual data
- agents: specialized AI agents used during code generation

These files guide the AI in generating code consistent with the project’s architecture.

### 3. Project-Level Continue Configuration
A `continue.yaml` file was added to the project root. It:
- Defines the AI model used for generation
- Reads the API key from the environment variable `OPENAI_API_KEY`
- Loads the `.continue` folder as part of the project’s context

This enables consistent and secure integration with the Continue extension.

### 4. AI Folder
An `ai/` directory was added for storing experimental files and scripts related to AI-assisted development.

### 5. Git Ignore Rules
A `.gitignore` file was added with entries appropriate for .NET, Visual Studio Code, and Continue configuration files. This prevents unnecessary or sensitive files from being committed.

## Next Steps
1. Define additional rules, contexts, and agents under the `.continue` directory.
2. Configure Continue to use a supported model such as GPT-4.1 for architecture and code generation.
3. Begin generating the system architecture using prompts specific to Clean Architecture, DDD, and CQRS.
