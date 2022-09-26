using Domain.Entities.Interfaces;

namespace Domain.Transactions.Interfaces
{
    public interface IUnitOfWork
    {
        Task<bool> SaveAggregateRootAsync(IAggregateRoot aggregateRoot, CancellationToken cancellationToken = default(CancellationToken));
    }
}
