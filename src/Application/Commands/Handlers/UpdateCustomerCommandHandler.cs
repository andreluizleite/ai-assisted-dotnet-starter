using MediatR;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Commands.Handlers;

public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, CustomerDto>
{
    private readonly ICustomerRepository _repository;
    public UpdateCustomerCommandHandler(ICustomerRepository repository)
        => _repository = repository;

    public async Task<CustomerDto> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = await _repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new Exception("Customer not found");
        customer.Update(request.FirstName, request.LastName, new Email(request.Email));
        await _repository.UpdateAsync(customer, cancellationToken);
        return new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email.Value,
            CreatedAt = customer.CreatedAt,
            UpdatedAt = customer.UpdatedAt
        };
    }
}
