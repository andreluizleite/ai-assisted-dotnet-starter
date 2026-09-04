using MediatR;

namespace CleanArchitecture.Application.Commands;

public sealed record DeleteCustomerCommand(Guid Id) : IRequest;
