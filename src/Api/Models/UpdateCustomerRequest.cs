namespace CleanArchitecture.Api.Models;

public sealed record UpdateCustomerRequest(
    string FirstName,
    string LastName,
    string Email);
