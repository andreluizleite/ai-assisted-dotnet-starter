using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.Repositories;
using CleanArchitecture.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Repositories;

public sealed class CustomerRepository(AppDbContext context) : ICustomerRepository
{
    public Task<Customer?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default) =>
        context.Customers.FirstOrDefaultAsync(customer => customer.Id == id, cancellationToken);

    public async Task<IReadOnlyCollection<Customer>> GetAllAsync(
        CancellationToken cancellationToken = default) =>
        await context.Customers
            .AsNoTracking()
            .OrderBy(customer => customer.LastName)
            .ThenBy(customer => customer.FirstName)
            .ToArrayAsync(cancellationToken);

    public Task<bool> EmailExistsAsync(
        string email,
        Guid? excludingCustomerId = null,
        CancellationToken cancellationToken = default)
    {
        return excludingCustomerId.HasValue
            ? context.Customers.AnyAsync(
                customer =>
                    customer.Email.Value == email &&
                    customer.Id != excludingCustomerId.Value,
                cancellationToken)
            : context.Customers.AnyAsync(
                customer => customer.Email.Value == email,
                cancellationToken);
    }

    public async Task AddAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        await context.Customers.AddAsync(customer, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        context.Customers.Update(customer);
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Customer customer, CancellationToken cancellationToken = default)
    {
        context.Customers.Remove(customer);
        await context.SaveChangesAsync(cancellationToken);
    }
}
