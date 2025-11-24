namespace CleanArchitecture.Domain.Entities;

using CleanArchitecture.Domain.ValueObjects;

public class Customer
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public Email Email { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    public Customer(Guid id, string firstName, string lastName, Email email)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        CreatedAt = DateTime.UtcNow;
    }

    public void Update(string firstName, string lastName, Email email)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        UpdatedAt = DateTime.UtcNow;
    }
}
