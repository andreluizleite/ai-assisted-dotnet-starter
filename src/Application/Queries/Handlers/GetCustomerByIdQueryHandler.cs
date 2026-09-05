using CleanArchitecture.Application.Common.Exceptions;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.Queries.Handlers;

public sealed class GetCustomerByIdQueryHandler(ICustomerRepository repository)
    : IRequestHandler<GetCustomerByIdQuery, CustomerDto>
{
    public async Task<CustomerDto> Handle(
        GetCustomerByIdQuery request,
        CancellationToken cancellationToken)
    {
        var customer = await repository.GetByIdAsync(request.Id, cancellationToken)
            ?? throw new CustomerNotFoundException(request.Id);

        return customer.ToDto();
    }
}
