using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.Commands.Handlers;

public sealed class DeleteCustomerCommandHandler(ICustomerRepository repository)
    : IRequestHandler<DeleteCustomerCommand>
{
    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new CustomerNotFoundException(request.Id);

        await repository.DeleteAsync(customer, cancellationToken);
    }
}
