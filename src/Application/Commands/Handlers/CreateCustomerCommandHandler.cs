using MediatR;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Domain.ValueObjects;

namespace CleanArchitecture.Application.Commands.Handlers;

public class CreateCustomerCommandHandler : IRequestHandler<CreateCustomerCommand, CustomerDto>
{
    private readonly ICustomerRepository _repository;
    public CreateCustomerCommandHandler(ICustomerRepository repository)
        => _repository = repository;

    public async Task<CustomerDto> Handle(CreateCustomerCommand request, CancellationToken cancellationToken)
    {
        var customer = new Customer(Guid.NewGuid(), request.FirstName, request.LastName, new Email(request.Email));
        await _repository.AddAsync(customer, cancellationToken);
        return new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email.Value,
            CreatedAt = customer.CreatedAt
        };
    }
}
