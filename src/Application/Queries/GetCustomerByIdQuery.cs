using CleanArchitecture.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Queries;

public sealed record GetCustomerByIdQuery(Guid Id) : IRequest<CustomerDto>;
