namespace CleanArchitecture.Application.Common.Exceptions;

public sealed class EmailAlreadyInUseException(string email)
    : Exception($"Email '{email}' is already assigned to another customer.")
{
    public string Email { get; } = email;
}
