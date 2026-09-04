namespace CleanArchitecture.Api.Models;

public sealed record CreateCustomerRequest(
    string FirstName,
    string LastName,
    string Email);
