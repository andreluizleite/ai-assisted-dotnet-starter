## Implemented Example: Customer

The Customer aggregate is already fully implemented with:
- Entity: Customer (Id, Name, Email, DateOfBirth, IsActive)
- EF Core mapping
- Seeding
- Repository implementation
- CQRS commands and queries
- API controller with CRUD endpoints

When generating new aggregates, follow the same patterns used by the Customer implementation:
- Same folder structure
- Same CQRS approach
- Same EF Core configuration style
- Same validation style (FluentValidation + domain rules)
