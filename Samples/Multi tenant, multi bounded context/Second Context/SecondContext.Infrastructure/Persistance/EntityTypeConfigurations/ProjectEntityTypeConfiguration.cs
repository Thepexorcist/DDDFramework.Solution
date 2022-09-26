using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecondContext.Domain.Aggregates.ProjectAggregate;
using SecondContext.Domain.Tenant;

namespace SecondContext.Infrastructure.Persistance
{
    public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("Project");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new ProjectId(x));
            builder.Property(x => x.Id).ValueGeneratedNever();
            builder.Property(x => x.TenantId)
                .HasConversion(x => x.Value, x => new TenantId(x));
            builder.Property(x => x.ProjectNumber);
            builder.Property(x => x.IsPublished);
            builder.Property(x => x.DisplayName);
            builder.HasMany(x => x.Documents)
                .WithOne()
                .HasForeignKey("ProjectId");
            builder.OwnsOne(x => x.AssignedProjectManager, assignedProjectManager =>
            {
                assignedProjectManager.ToTable("ProjectAssignedProjectManager");
                assignedProjectManager.WithOwner().HasForeignKey("ProjectId");
                assignedProjectManager.Property(x => x.EmployeeNumber);
                assignedProjectManager.Property(x => x.FirstName);
                assignedProjectManager.Property(x => x.LastName);
            });
            builder.OwnsMany(x => x.AssignedElectricians, assignedElectricians =>
            {
                assignedElectricians.ToTable("ProjectAssignedElectrician");
                assignedElectricians.WithOwner().HasForeignKey("ProjectId");
                assignedElectricians.Property(x => x.EmployeeNumber);
                assignedElectricians.Property(x => x.FirstName);
                assignedElectricians.Property(x => x.LastName);
            });

            builder.Ignore(x => x.DomainEvents);
        }
    }
}
