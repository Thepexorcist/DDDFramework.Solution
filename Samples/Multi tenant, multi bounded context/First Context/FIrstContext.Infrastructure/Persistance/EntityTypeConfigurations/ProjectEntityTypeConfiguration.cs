using FirstContext.Domain.Aggregates.WorkspaceAggregate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FirstContext.Infrastructure.Persistance
{
    public class ProjectEntityTypeConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.ToTable("WorkspaceProject");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new ProjectId(x))
                .ValueGeneratedOnAdd()
                .Metadata.SetBeforeSaveBehavior(PropertySaveBehavior.Ignore);
            builder.Property(x => x.UniqueProjectNumber);
            builder.Property(x => x.Name);
            builder.Property(x => x.Created);
            builder.Property(x => x.IsPublished);
        }
    }
}
