using Domain.Entities.Interfaces;
using Domain.Infrastructure.Messaging.EventualConsistency;
using Domain.Infrastructure.Messaging.EventualConsistency.Interfaces;
using Domain.Infrastructure.Messaging.Interfaces;
using Domain.Transactions.Interfaces;
using FirstContext.Domain.Aggregates.TenantAggregate;
using FirstContext.Domain.Aggregates.WorkspaceAggregate;
using FirstContext.Domain.Aggregates.WorkspaceAggregate.Entities;
using Microsoft.EntityFrameworkCore;

namespace FirstContext.Infrastructure.Persistance
{
    public class FirstContextDbContext : DbContext, IUnitOfWork, IOutboxMessageDbContext
    {
        #region Fields

        private readonly IDomainEventDispatcher<FirstContextDbContext> _domainEventDispatcher;

        #endregion

        #region Properties

        public DbSet<OutboxMessage> OutboxMessages { get; set; }
        public DbSet<Tenant> Tenant { get; set; }
        public DbSet<Workspace> Workspaces { get; set; }
        public DbSet<Project> Projects { get; set; }

        #endregion

        public FirstContextDbContext(DbContextOptions<FirstContextDbContext> options, IDomainEventDispatcher<FirstContextDbContext> domainEventDispatcher) : base(options)
        {
            _domainEventDispatcher = domainEventDispatcher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration<OutboxMessage>(new OutboxMessageEntityTypeConfiguration("dbo"));
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FirstContextDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }

        public async Task<bool> SaveAggregateRootAsync(IAggregateRoot aggregateRoot, CancellationToken cancellationToken = default)
        {
            await _domainEventDispatcher.DispatchDomainEventsAsync(aggregateRoot, this);
            await base.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
