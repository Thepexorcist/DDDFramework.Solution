using Domain.Entities;
using Domain.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories.Interfaces
{
    public interface IRepository<TAggregateRoot, TIdentity>
        where TAggregateRoot : AggregateRootBase<TIdentity>
    {
        IUnitOfWork UnitOfWork { get; }
        Task<TAggregateRoot> GetAsync(TIdentity id);
        Task UpdateAsync(TAggregateRoot aggregateRoot);
        Task AddAsync(TAggregateRoot aggregateRoot);
    }
}
