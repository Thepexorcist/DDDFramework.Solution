using Domain.Entities.Interfaces;
using Domain.Infrastructure.Messaging.Interfaces;
using Domain.Transactions.Interfaces;
using Microsoft.EntityFrameworkCore;
using SecondContext.Domain.Aggregates.ProjectAggregate;
using SecondContext.Domain.Aggregates.ProjectAggregate.Entities;

namespace SecondContext.Infrastructure.Persistance
{
    public class SecondContextDbContext : DbContext, IUnitOfWork
    {
        #region Fields

        private readonly IDomainEventDispatcher<SecondContextDbContext> _domainEventDispatcher;

        #endregion

        #region Properties

        public DbSet<Project> Projects { get; set; }
        public DbSet<Document> Documents { get; set; }

        #endregion

        public SecondContextDbContext(DbContextOptions<SecondContextDbContext> options, IDomainEventDispatcher<SecondContextDbContext> domainEventDispatcher) : base(options)
        {
            _domainEventDispatcher = domainEventDispatcher;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SecondContextDbContext).Assembly);

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
