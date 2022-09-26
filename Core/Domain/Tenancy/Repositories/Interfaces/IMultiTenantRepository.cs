using Domain.Transactions.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tenancy.Repositories.Interfaces
{
    public interface IMultiTenantRepository<TMultitenantAggregateRoot, TTenantIdentity, TIdentity>
        where TMultitenantAggregateRoot : MultiTenantAggregateRootBase<TTenantIdentity, TIdentity>
    {
        IUnitOfWork UnitOfWork { get; }
        Task<TMultitenantAggregateRoot> GetAsync(TTenantIdentity tenantId, TIdentity id);
        Task<List<TMultitenantAggregateRoot>> GetAllAsync(TTenantIdentity tenantId);
        Task UpdateAsync(TMultitenantAggregateRoot aggregateRoot);
        Task AddAsync(TMultitenantAggregateRoot aggregateRoot);
    }
}
