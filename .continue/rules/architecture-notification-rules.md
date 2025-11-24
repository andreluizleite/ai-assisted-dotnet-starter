# Notification Rules

## Overview
The Domain and Application layers must not depend on AWS, SNS, SQS, WebSockets, SignalR, or any external notification mechanism.
Notifications must be abstracted behind interfaces and implemented only in the Infrastructure layer.

## Core Rules

### 1. Notification Abstraction
- The Application layer publishes integration events to an Outbox table.
- A single interface must be used for dispatching notifications:
  
  INotificationPublisher:
      - Implemented only in Infrastructure
      - Used by the Outbox dispatcher
      - Never referenced by Domain

### 2. Local Development
- In local development, notifications must be handled by a FakeNotificationPublisher.
- FakeNotificationPublisher:
  - Logs messages to console or stores in memory
  - Does not call external services
  - Must implement INotificationPublisher

### 3. AWS in the Future
- AWS SNS/SQS support will be added in Infrastructure only.
- No reference to AWS SDK must appear in Domain or Application.
- The implementation will replace or wrap FakeNotificationPublisher.

### 4. Notification Events
- Notifications must be triggered only by integration events (e.g., PostDraftGenerated, PostApproved, PostGenerationFailed).
- Integration events must be:
  - Created in Application
  - Persisted via Outbox
  - Dispatched asynchronously by Infrastructure

### 5. No Notification Logic in Domain
- Domain entities, value objects, and services must not send or publish notifications.
- Domain events only trigger workflow logic in Application.
- Application layer decides which integration events to create.
