using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Messaging.Idempotency
{
    public class IntegrationRequestEntityTypeConfiguration : IEntityTypeConfiguration<IntegrationRequest>
    {
        #region Fields

        private readonly string _schemaName;

        #endregion

        public IntegrationRequestEntityTypeConfiguration(string schemaName)
        {
            _schemaName = schemaName;
        }

        public void Configure(EntityTypeBuilder<IntegrationRequest> builder)
        {
            builder.ToTable("IntegrationRequest", _schemaName);

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedNever();
        }
    }
}
