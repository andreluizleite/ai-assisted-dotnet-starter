using CleanArchitecture.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Queries;

public sealed record GetAllCustomersQuery : IRequest<IReadOnlyCollection<CustomerDto>>;
