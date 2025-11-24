using {{Namespace}}.Domain.Entities;

namespace {{Namespace}}.Domain.Repositories;

// Base repository interface for an aggregate root
// Replace {{EntityName}}

public interface I{{EntityName}}Repository
{
    Task<{{EntityName}}?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<{{EntityName}}>> GetAllAsync(CancellationToken cancellationToken = default);
    Task AddAsync({{EntityName}} entity, CancellationToken cancellationToken = default);
    Task UpdateAsync({{EntityName}} entity, CancellationToken cancellationToken = default);
    Task DeleteAsync({{EntityName}} entity, CancellationToken cancellationToken = default);
}
