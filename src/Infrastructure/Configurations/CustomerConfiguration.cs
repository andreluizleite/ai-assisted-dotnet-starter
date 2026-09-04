using CleanArchitecture.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Configurations;

public sealed class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");
        builder.HasKey(customer => customer.Id);
        builder.Property(customer => customer.FirstName).IsRequired().HasMaxLength(100);
        builder.Property(customer => customer.LastName).IsRequired().HasMaxLength(100);
        builder.Property(customer => customer.CreatedAt).IsRequired();
        builder.Property(customer => customer.UpdatedAt);

        builder.OwnsOne(customer => customer.Email, email =>
        {
            email.Property(value => value.Value)
                .IsRequired()
                .HasMaxLength(254)
                .HasColumnName("Email");
            email.HasIndex(value => value.Value).IsUnique();
        });
    }
}
