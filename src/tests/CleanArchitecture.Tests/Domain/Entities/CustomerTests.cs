using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Tests.Domain.Entities;

public sealed class CustomerTests
{
    [Fact]
    public void Create_NormalizesNamesAndSetsCreationTime()
    {
        var createdAt = new DateTimeOffset(2026, 9, 4, 12, 0, 0, TimeSpan.Zero);

        var customer = Customer.Create(
            "  Ada ",
            " Lovelace  ",
            new Email("ADA@example.com"),
            createdAt);

        Assert.NotEqual(Guid.Empty, customer.Id);
        Assert.Equal("Ada", customer.FirstName);
        Assert.Equal("Lovelace", customer.LastName);
        Assert.Equal("ada@example.com", customer.Email.Value);
        Assert.Equal(createdAt, customer.CreatedAt);
        Assert.Null(customer.UpdatedAt);
    }

    [Fact]
    public void Create_RejectsBlankName()
    {
        var exception = Assert.Throws<ArgumentException>(
            () => Customer.Create(" ", "Lovelace", new Email("ada@example.com")));

        Assert.Equal("firstName", exception.ParamName);
    }

    [Fact]
    public void Update_ChangesContactDataAndSetsUpdateTime()
    {
        var customer = Customer.Create("Ada", "Lovelace", new Email("ada@example.com"));
        var updatedAt = new DateTimeOffset(2026, 9, 5, 12, 0, 0, TimeSpan.Zero);

        customer.Update("Grace", "Hopper", new Email("grace@example.com"), updatedAt);

        Assert.Equal("Grace", customer.FirstName);
        Assert.Equal("Hopper", customer.LastName);
        Assert.Equal("grace@example.com", customer.Email.Value);
        Assert.Equal(updatedAt, customer.UpdatedAt);
    }
}
