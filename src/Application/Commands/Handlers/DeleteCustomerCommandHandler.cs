using MediatR;
using CleanArchitecture.Domain.Repositories;

namespace CleanArchitecture.Application.Commands.Handlers;

public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand>
{
    private readonly ICustomerRepository _repository;
    public DeleteCustomerCommandHandler(ICustomerRepository repository)
        => _repository = repository;

    public async Task Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new Exception("Customer not found");
        await _repository.DeleteAsync(customer, cancellationToken);
    }
}
