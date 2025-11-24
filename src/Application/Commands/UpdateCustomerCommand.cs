using MediatR;
using CleanArchitecture.Application.Dtos;

namespace CleanArchitecture.Application.Commands;

public class UpdateCustomerCommand : IRequest<CustomerDto>
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
}
