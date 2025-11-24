using MediatR;

namespace CleanArchitecture.Application.Commands;

public class DeleteCustomerCommand : IRequest
{
    public Guid Id { get; set; }
}
