using CleanArchitecture.Application.Dtos;
using MediatR;

namespace CleanArchitecture.Application.Commands;

public sealed record CreateCustomerCommand(
    string FirstName,
    string LastName,
    string Email) : IRequest<CustomerDto>;
