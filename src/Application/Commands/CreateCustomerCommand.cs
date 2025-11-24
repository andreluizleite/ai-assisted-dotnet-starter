using MediatR;
using CleanArchitecture.Application.Dtos;

namespace CleanArchitecture.Application.Commands;

public class CreateCustomerCommand : IRequest<CustomerDto>
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string Email { get; set; } = default!;
}
