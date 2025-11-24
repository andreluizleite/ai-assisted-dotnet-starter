# Domain: Customer

## Entity: Customer
Properties:
- Id (Guid)
- Name (string, required, max 200)
- Email (string, required, valid format, unique)
- DateOfBirth (DateTime?, optional)
- IsActive (bool)

## Business Rules
- Name is required.
- Email is required.
- Email must be unique in the system.
- Email must follow a valid email format.
- An inactive customer (IsActive = false) cannot be used to create new Orders in the future.

## Value Object: Email
- Stores validated email address.
- Immutable.

## CQRS Commands
- CreateCustomerCommand
- UpdateCustomerCommand
- DeleteCustomerCommand

## CQRS Queries
- GetCustomerByIdQuery
- GetAllCustomersQuery
