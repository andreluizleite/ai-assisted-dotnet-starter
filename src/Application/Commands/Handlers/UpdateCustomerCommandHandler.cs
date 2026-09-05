using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Domain.ValueObjects;
using MediatR;

namespace CleanArchitecture.Application.Commands.Handlers;

public sealed class UpdateCustomerCommandHandler(ICustomerRepository repository)
    : IRequestHandler<UpdateCustomerCommand, CustomerDto>
{
    public async Task<CustomerDto> Handle(
        UpdateCustomerCommand request,
        CancellationToken cancellationToken)
    {
        var customer = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new CustomerNotFoundException(request.Id);

        var email = new Email(request.Email);
        if (await repository.EmailExistsAsync(email.Value, request.Id, cancellationToken))
        {
            throw new EmailAlreadyInUseException(email.Value);
        }

        customer.Update(request.FirstName, request.LastName, email);
        await repository.UpdateAsync(customer, cancellationToken);

        return customer.ToDto();
    }
}
