using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Tests.Domain.ValueObjects;

public sealed class EmailTests
{
    [Theory]
    [InlineData("ADA@EXAMPLE.COM", "ada@example.com")]
    [InlineData("  grace@example.com ", "grace@example.com")]
    public void Constructor_NormalizesValidAddress(string input, string expected)
    {
        var email = new Email(input);

        Assert.Equal(expected, email.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("not-an-email")]
    [InlineData("@example.com")]
    public void Constructor_RejectsInvalidAddress(string input)
    {
        Assert.Throws<ArgumentException>(() => new Email(input));
    }
}
