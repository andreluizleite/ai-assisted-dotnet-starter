using MediatR;
using CleanArchitecture.Application.Dtos;
using CleanArchitecture.Domain.Repositories;

namespace CleanArchitecture.Application.Queries.Handlers;

public class GetAllCustomersQueryHandler : IRequestHandler<GetAllCustomersQuery, List<CustomerDto>>
{
    private readonly ICustomerRepository _repository;
    public GetAllCustomersQueryHandler(ICustomerRepository repository)
        => _repository = repository;

    public async Task<List<CustomerDto>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
    {
        var customers = await _repository.GetAllAsync(cancellationToken);
        return customers.Select(customer => new CustomerDto
        {
            Id = customer.Id,
            FirstName = customer.FirstName,
            LastName = customer.LastName,
            Email = customer.Email.Value,
            CreatedAt = customer.CreatedAt,
            UpdatedAt = customer.UpdatedAt
        }).ToList();
    }
}
