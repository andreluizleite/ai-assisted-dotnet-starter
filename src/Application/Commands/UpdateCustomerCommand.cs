using CleanArchitecture.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Commands;

public sealed record UpdateCustomerCommand(
    Guid Id,
    string FirstName,
    string LastName,
    string Email) : IRequest<CustomerDto>;
