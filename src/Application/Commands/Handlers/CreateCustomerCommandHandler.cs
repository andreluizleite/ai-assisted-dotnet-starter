using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Domain.ValueObjects;
using MediatR;

namespace CleanArchitecture.Application.Commands.Handlers;

public sealed class CreateCustomerCommandHandler(ICustomerRepository repository)
    : IRequestHandler<CreateCustomerCommand, CustomerDto>
{
    public async Task<CustomerDto> Handle(
        CreateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var email = new Email(request.Email);
        if (await repository.EmailExistsAsync(email.Value, cancellationToken: cancellationToken))
        {
            throw new EmailAlreadyInUseException(email.Value);
        }

        var customer = Customer.Create(request.FirstName, request.LastName, email);
        await repository.AddAsync(customer, cancellationToken);

        return customer.ToDto();
    }
}
