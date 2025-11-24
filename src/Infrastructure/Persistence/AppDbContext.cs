using Microsoft.EntityFrameworkCore;
using CleanArchitecture.Domain.Entities;

namespace CleanArchitecture.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Customer> Customers { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Seed data
        var customer1Id = Guid.Parse("11111111-1111-1111-1111-111111111111");
        var customer2Id = Guid.Parse("22222222-2222-2222-2222-222222222222");
        var createdAt = new DateTime(2024, 1, 1);

        modelBuilder.Entity<Customer>().HasData(
            new
            {
                Id = customer1Id,
                FirstName = "John",
                LastName = "Doe",
                CreatedAt = createdAt
            },
            new
            {
                Id = customer2Id,
                FirstName = "Jane",
                LastName = "Smith",
                CreatedAt = createdAt
            }
        );

        modelBuilder.Entity<Customer>().OwnsOne(c => c.Email).HasData(
            new
            {
                CustomerId = customer1Id,
                Value = "john.doe@example.com"
            },
            new
            {
                CustomerId = customer2Id,
                Value = "jane.smith@example.com"
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}
