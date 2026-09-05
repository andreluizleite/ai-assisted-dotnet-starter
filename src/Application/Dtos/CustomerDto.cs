namespace CleanArchitecture.Application.Dtos;

public sealed record CustomerDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email,
    DateTimeOffset CreatedAt,
    DateTimeOffset? UpdatedAt);
