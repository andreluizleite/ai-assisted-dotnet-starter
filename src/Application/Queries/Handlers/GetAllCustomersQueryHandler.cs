using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Domain.Repositories;
using MediatR;

namespace CleanArchitecture.Application.Queries.Handlers;

public sealed class GetAllCustomersQueryHandler(ICustomerRepository repository)
    : IRequestHandler<GetAllCustomersQuery, IReadOnlyCollection<CustomerDto>>
{
    public async Task<IReadOnlyCollection<CustomerDto>> Handle(
        GetAllCustomersQuery request,
        CancellationToken cancellationToken)
    {
        var customers = await repository.GetAllAsync(cancellationToken);
        return customers.Select(customer => customer.ToDto()).ToArray();
    }
}
