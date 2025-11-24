[Context]     : ContentAuthoring
[Use Case ID] : UC-POST-03
[Name]        : ApprovePost
[Type]        : Command (MediatR)
[Sync/Async]  : Sync (HTTP)

[Goal]
User approves a Draft post. After approval, a notification is published indicating that the post is approved.

[Input DTO]
Name   : ApprovePostCommand
Fields:
  - Guid TenantId
  - Guid UserId
  - Guid PostId

[Domain Flow]
1) Load Post by PostId and TenantId.
2) Ensure Post.Status == Draft.
3) Set:
   - Post.Status = Approved
   - Post.ApprovedByUserId = UserId
   - Post.ApprovedAt = now
4) Save Post.
5) Append PostApprovedIntegrationEvent to Outbox.

[Notification / Integration Event]
- Event: PostApprovedIntegrationEvent
- Fields:
  - EventId
  - OccurredAt
  - TenantId
  - PostId
  - ApprovedByUserId
  - Status = "Approved"

[Usage]
- Same mechanism: INotificationPublisher / bus.
- Locally: fake/mock.
- Future: mapped to AWS SNS topic.
