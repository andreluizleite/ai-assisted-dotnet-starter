using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Domain.Repositories;

public interface ICustomerRepository
{
    Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyCollection<Customer>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<bool> EmailExistsAsync(
        string email,
        Guid? excludingCustomerId = null,
        CancellationToken cancellationToken = default);
    Task AddAsync(Customer customer, CancellationToken cancellationToken = default);
    Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default);
    Task DeleteAsync(Customer customer, CancellationToken cancellationToken = default);
}
