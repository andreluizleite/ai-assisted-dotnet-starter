[Context]     : ContentAuthoring
[Use Case ID] : UC-POST-02
[Name]        : ProcessPostGenerationJob
[Type]        : Command (MediatR)
[Sync/Async]  : Async (background worker / queue)

[Goal]
Take a pending PostGenerationJob, fetch news for the topic, call AI to generate the post,
create a Post, and send a notification that the post is ready (draft or approved) or failed.

[Input DTO]
Name     : ProcessPostGenerationJobCommand
Fields:
  - Guid JobId

[Domain Flow]
1) Load PostGenerationJob by JobId.
2) If Job.Status is not Pending → do nothing (idempotent).
3) Set Job.Status = InProgress.
4) Call INewsSearchService to get latest news for Job.Topic.
5) Call IAiPostGenerator to build the post content.
6) Create Post aggregate:
   - Id (PostId)
   - TenantId
   - JobId
   - Topic
   - Platform
   - Content
   - Status:
     - Approved if Job.AutoApprove == true
     - Draft if Job.AutoApprove == false
7) Update Job:
   - Status = Completed
   - CompletedAt = now
8) Commit transaction (Job + Post + Outbox).

[Notifications / Integration Events]
After the transaction, an Outbox worker (or the handler itself writing to Outbox) produces:

1) If success AND AutoApprove == false:
   - Event: PostDraftGeneratedIntegrationEvent
   - Fields:
     - EventId
     - OccurredAt
     - TenantId
     - JobId
     - PostId
     - Status = "DraftGenerated"

2) If success AND AutoApprove == true:
   - Event: PostApprovedIntegrationEvent
   - Fields:
     - EventId
     - OccurredAt
     - TenantId
     - JobId
     - PostId
     - Status = "Approved"

3) If any failure (news search or AI generation):
   - Set Job.Status = Failed, Job.FailureReason = ...
   - Event: PostGenerationFailedIntegrationEvent
   - Fields:
     - EventId
     - OccurredAt
     - TenantId
     - JobId
     - FailureReason

[How Notifications Are Used]
- The Infrastructure layer implements an INotificationPublisher (or IMessageBusPublisher).
- When Outbox events are dispatched:
  - Locally: they can be logged, stored in-memory, or pushed to a fake bus.
  - Future: they’ll go to AWS SNS topics (e.g., "post-generation-events").
- The consumer (UI or another service) subscribes to these notifications to know:
  - “A draft is ready for review” or
  - “A post was auto-approved” or
  - “Generation failed”.

[Repositories / Services]
- IPostGenerationJobRepository
- IPostRepository
- IOutboxEventRepository
- INewsSearchService
- IAiPostGenerator
