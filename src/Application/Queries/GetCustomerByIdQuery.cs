using MediatR;
using CleanArchitecture.Application.Dtos;

namespace CleanArchitecture.Application.Queries;

public class GetCustomerByIdQuery : IRequest<CustomerDto?>
{
    public Guid Id { get; set; }
    public GetCustomerByIdQuery(Guid id) => Id = id;
}
