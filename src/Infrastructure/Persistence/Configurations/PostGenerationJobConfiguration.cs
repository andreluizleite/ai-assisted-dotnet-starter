using CleanArchitecture.Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using CleanArchitecture.Domain.Enums;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations
{
    public class PostGenerationJobConfiguration : IEntityTypeConfiguration<PostGenerationJob>
    {
        public void Configure(EntityTypeBuilder<PostGenerationJob> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.TenantId).IsRequired();
            builder.Property(x => x.RequestedByUserId).IsRequired();
            builder.Property(x => x.Topic).IsRequired().HasMaxLength(200);
            builder.Property(x => x.Platform).IsRequired();
            builder.Property(x => x.AutoApprove).IsRequired();
            builder.Property(x => x.Status)
                .HasConversion<string>()
                .IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired();

            builder.HasData(new
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                TenantId = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                RequestedByUserId = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Topic = "AI",
                Platform = PlatformType.LinkedIn,
                AutoApprove = true,
                Status = PostGenerationJobStatus.Pending,
                CreatedAt = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero)
            });
        }
        }
}

