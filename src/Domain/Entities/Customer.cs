using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Domain.Entities;

public sealed class Customer
{
    private Customer()
    {
    }

    private Customer(Guid id, string firstName, string lastName, Email email, DateTimeOffset createdAt)
    {
        Id = id;
        FirstName = NormalizeName(firstName, nameof(firstName));
        LastName = NormalizeName(lastName, nameof(lastName));
        Email = email;
        CreatedAt = createdAt;
    }

    public Guid Id { get; private set; }

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public Email Email { get; private set; } = null!;

    public DateTimeOffset CreatedAt { get; private set; }

    public DateTimeOffset? UpdatedAt { get; private set; }

    public static Customer Create(
        string firstName,
        string lastName,
        Email email,
        DateTimeOffset? createdAt = null)
    {
        ArgumentNullException.ThrowIfNull(email);

        return new Customer(
            Guid.NewGuid(),
            firstName,
            lastName,
            email,
            createdAt ?? DateTimeOffset.UtcNow);
    }

    public void Update(
        string firstName,
        string lastName,
        Email email,
        DateTimeOffset? updatedAt = null)
    {
        ArgumentNullException.ThrowIfNull(email);

        FirstName = NormalizeName(firstName, nameof(firstName));
        LastName = NormalizeName(lastName, nameof(lastName));
        Email = email;
        UpdatedAt = updatedAt ?? DateTimeOffset.UtcNow;
    }

    private static string NormalizeName(string value, string parameterName)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value, parameterName);

        var normalized = value.Trim();
        if (normalized.Length > 100)
        {
            throw new ArgumentOutOfRangeException(parameterName, "Names cannot exceed 100 characters.");
        }

        return normalized;
    }
}
