using Domain.Aggregates;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations
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
        }
    }
}
