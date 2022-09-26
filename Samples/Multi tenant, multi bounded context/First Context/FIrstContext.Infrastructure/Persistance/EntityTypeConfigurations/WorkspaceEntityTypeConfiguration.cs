using FirstContext.Domain.Aggregates.TenantAggregate;
using FirstContext.Domain.Aggregates.WorkspaceAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstContext.Infrastructure.Persistance
{
    public class WorkspaceEntityTypeConfiguration : IEntityTypeConfiguration<Workspace>
    {
        public void Configure(EntityTypeBuilder<Workspace> builder)
        {
            builder.ToTable("Workspace");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new WorkspaceId(x));
            builder.Property(e => e.Id).ValueGeneratedNever();
            builder.Property(x => x.TenantId)
                .HasConversion(x => x.Value, x => new TenantId(x));
            builder.Property(x => x.Name);
            builder.Property(x => x.IsActive);
            builder.HasMany(x => x.Projects)
                .WithOne()
                .HasForeignKey("WorkspaceId");
            builder.OwnsMany(x => x.LogEntries, logEntries =>
            {
                logEntries.ToTable("WorkspaceLogEntry");
                logEntries.WithOwner().HasForeignKey("WorkspaceId");
                logEntries.Property(x => x.Created).HasColumnName("Created");
                logEntries.Property(x => x.Action).HasColumnName("Action");
            });

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
