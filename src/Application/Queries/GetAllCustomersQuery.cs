using MediatR;
using CleanArchitecture.Application.Dtos;

namespace CleanArchitecture.Application.Queries;

public class GetAllCustomersQuery : IRequest<List<CustomerDto>>
{
}
