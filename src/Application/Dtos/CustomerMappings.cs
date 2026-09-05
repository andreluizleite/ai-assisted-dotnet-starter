using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Application.Dtos;

internal static class CustomerMappings
{
    public static CustomerDto ToDto(this Customer customer) =>
        new(
            customer.Id,
            customer.FirstName,
            customer.LastName,
            customer.Email.Value,
            customer.CreatedAt,
            customer.UpdatedAt);
}
