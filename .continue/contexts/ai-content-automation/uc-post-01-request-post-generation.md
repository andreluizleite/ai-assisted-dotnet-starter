[Context]     : ContentAuthoring
[Use Case ID] : UC-POST-01
[Name]        : RequestPostGeneration
[Type]        : Command (MediatR)
[Sync/Async]  : Sync (HTTP request → immediate response)

[Goal]
User requests the creation of a new AI-generated post based on the latest news for a topic.
We create a PostGenerationJob in status Pending and return its JobId. The actual news fetch + AI call is done asynchronously.

[Input DTO]
Namespace suggestion: Application/Commands/RequestPostGeneration
Name              : RequestPostGenerationCommand
Fields:
  - Guid TenantId              // from auth, but passed to handler
  - Guid UserId                // who requested it
  - string Topic               // e.g. "AI", "FinTech"
  - PlatformType Platform      // e.g. LinkedIn, Instagram
  - bool AutoApprove           // if true, post will be auto-approved when AI finishes
  - string? Tone               // e.g. "Professional", "Casual"
  - string? Language           // e.g. "en", "pt-BR"
  - string? AdditionalInstructions

[Output DTO]
Namespace: Application/DTOs
Name     : PostGenerationJobDto
Fields:
  - Guid JobId
  - string Status       // "Pending"
  - bool AutoApprove
  - DateTimeOffset CreatedAt

[Domain Model Impact]
- Create a PostGenerationJob aggregate in the Domain layer:
  - Id (JobId)
  - TenantId
  - RequestedByUserId
  - Topic
  - Platform
  - AutoApprove
  - Status = Pending
  - CreatedAt

[Domain Invariants]
- TenantId cannot be empty.
- Topic cannot be null/empty.
- Job initially must be in status Pending.

[Repositories Used]
- IPostGenerationJobRepository (Domain/Interfaces)

[Side Effects]
- Persist PostGenerationJob.
- Optionally append a PostGenerationRequestedIntegrationEvent to Outbox, to trigger async processing.

[Validation Rules]
- If Topic is empty → validation error.
- If TenantId or UserId is empty → validation error.
