using FirstContext.Domain.Aggregates.TenantAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstContext.Infrastructure.Persistance
{
    public class TenantEntityTypeConfiguration : IEntityTypeConfiguration<Tenant>
    {
        public void Configure(EntityTypeBuilder<Tenant> builder)
        {
            builder.ToTable("Tenant");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new TenantId(x));
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(x => x.Name);
            builder.Property(x => x.IsActive);

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
