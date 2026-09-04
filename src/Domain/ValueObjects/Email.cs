using System.Net.Mail;

namespace CleanArchitecture.Domain.ValueObjects;

public sealed record Email
{
    public Email(string value)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(value);

        var normalized = value.Trim().ToLowerInvariant();
        if (normalized.Length > 254 || !MailAddress.TryCreate(normalized, out var parsed) || parsed.Address != normalized)
        {
            throw new ArgumentException("A valid email address is required.", nameof(value));
        }

        Value = normalized;
    }

    public string Value { get; }

    public override string ToString() => Value;
}
