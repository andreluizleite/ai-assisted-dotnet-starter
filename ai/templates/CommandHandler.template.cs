using MediatR;
using {{Namespace}}.Domain.Entities;
using {{Namespace}}.Domain.Repositories;

namespace {{Namespace}}.Application.{{EntityName}}s.Commands;

// Template for a basic Create command handler
// Replace {{EntityName}} and namespaces

public record Create{{EntityName}}Command(
    // TODO: Add command parameters
) : IRequest<Guid>;

public class Create{{EntityName}}CommandHandler
    : IRequestHandler<Create{{EntityName}}Command, Guid>
{
    private readonly I{{EntityName}}Repository _repository;

    public Create{{EntityName}}CommandHandler(I{{EntityName}}Repository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(
        Create{{EntityName}}Command request,
        CancellationToken cancellationToken)
    {
        // TODO: Map request to entity using domain factory
        var entity = /* {{EntityName}}.Create(...) */ default({{EntityName}})!;

        await _repository.AddAsync(entity, cancellationToken);

        return entity.Id;
    }
}
