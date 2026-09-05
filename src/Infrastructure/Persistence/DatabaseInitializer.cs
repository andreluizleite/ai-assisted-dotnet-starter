using CleanArchitecture.Domain.Entities;
using CleanArchitecture.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CleanArchitecture.Infrastructure.Persistence;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(
        AppDbContext context,
        CancellationToken cancellationToken = default)
    {
        await context.Database.EnsureCreatedAsync(cancellationToken);

        if (await context.Customers.AnyAsync(cancellationToken))
        {
            return;
        }

        var createdAt = new DateTimeOffset(2026, 1, 1, 12, 0, 0, TimeSpan.Zero);
        context.Customers.AddRange(
            Customer.Create("Ada", "Lovelace", new Email("ada@example.com"), createdAt),
            Customer.Create("Alan", "Turing", new Email("alan@example.com"), createdAt));

        await context.SaveChangesAsync(cancellationToken);
    }
}
