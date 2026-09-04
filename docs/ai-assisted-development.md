# AI-assisted development playbook

## What agentic means here

AI assists the engineering process; it is not part of the runtime application. The
repository gives coding agents durable context through AGENTS.md, executable examples
and automated checks. This makes the result portable across tools instead of coupling the
architecture to a single model or editor extension.

## Suggested roles

For a change large enough to benefit from parallel review, separate these responsibilities:

1. **Analyst:** turns the request into scope, acceptance criteria and explicit exclusions.
2. **Implementer:** changes the smallest vertical slice that satisfies the criteria.
3. **Reviewer:** checks dependency direction, security, error behavior and unnecessary complexity.
4. **Tester:** adds boundary and integration scenarios and verifies the documented commands.

One agent may perform all roles for a small change. Multiple agents are useful only when
their work can be independently verified.

## Guardrails

- Business rules remain deterministic C#.
- AI does not approve its own scope or merge.
- Generated code must follow the same review and test path as human-written code.
- Claims in documentation must be backed by executable code.
- No secret is requested or copied into prompts, logs or commits.
- Failed checks are evidence to fix the implementation, not checks to disable.

## Change template

~~~text
Goal:
Business behavior:
In scope:
Out of scope:
Acceptance scenarios:
Architecture boundaries affected:
Security and privacy considerations:
Commands used as evidence:
~~~

## Example: adding a customer query

- Analyst defines filters, ordering and empty-result behavior.
- Implementer adds a query and handler in Application, then updates the repository port
  only if persistence-specific behavior is required.
- Reviewer ensures the query does not mutate state and no EF Core type leaks inward.
- Tester covers handler behavior and the HTTP contract.

## Interview explanation

The key point is not "AI wrote the code." The engineering value is the controlled loop:
durable context, narrow tasks, deterministic architecture rules, independent tests,
human review and traceable pull requests. This reduces inconsistency while preserving
accountability.
