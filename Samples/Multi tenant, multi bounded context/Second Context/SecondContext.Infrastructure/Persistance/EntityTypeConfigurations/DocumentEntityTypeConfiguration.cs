using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SecondContext.Domain.Aggregates.ProjectAggregate.Entities;

namespace SecondContext.Infrastructure.Persistance
{
    public class DocumentEntityTypeConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("ProjectDocument");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .HasConversion(x => x.Value, x => new DocumentId(x));
            builder.Property(x => x.Name);
            builder.Property(x => x.Description);
            builder.Property(x => x.URL);
            builder.Property(x => x.Version);
            builder.OwnsMany(x => x.RevisionHistory, revisionHistory =>
            {
                revisionHistory.ToTable("ProjectDocumentRevision");
                revisionHistory.WithOwner().HasForeignKey("DocumentId");
                revisionHistory.Property(x => x.Comment);
                revisionHistory.Property(x => x.Version);
            });
        }
    }
}
