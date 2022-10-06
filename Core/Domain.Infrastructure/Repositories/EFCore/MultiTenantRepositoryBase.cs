using Domain.Infrastructure.Tenancy.Interfaces;
using Domain.Tenancy;
using Domain.Tenancy.Repositories.Interfaces;
using Domain.Transactions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Repositories.EFCore
{
    public abstract class MultiTenantRepositoryBase<TContext, TMultiTenantAggregateRootBase, TTenantIdentity, TIdentity> :
        IMultiTenantRepository<TMultiTenantAggregateRootBase, TTenantIdentity, TIdentity>
        where TContext : DbContext, IUnitOfWork
        where TMultiTenantAggregateRootBase : MultiTenantAggregateRootBase<TTenantIdentity, TIdentity>
    {
        #region Fields

        protected readonly TContext _context;
        private readonly ITenantRepositoryFilter<TTenantIdentity> _tenantRepositoryFilter;

        #endregion

        #region Properties

        public IUnitOfWork UnitOfWork => _context;

        #endregion

        #region Constructors

        public MultiTenantRepositoryBase(TContext context, ITenantRepositoryFilter<TTenantIdentity> tenantRepositoryFilter)
        {
            _context = context;
            _tenantRepositoryFilter = tenantRepositoryFilter;
        }

        #endregion

        public virtual async Task AddAsync(TMultiTenantAggregateRootBase aggregateRoot)
        {
            _tenantRepositoryFilter.FilterAdded(aggregateRoot);

            await _context.AddAsync(aggregateRoot);
        }

        public virtual async Task<TMultiTenantAggregateRootBase> GetAsync(TTenantIdentity tenantId, TIdentity id)
        {
            var aggregates = _context.Set<TMultiTenantAggregateRootBase>();

            var result = await _context.FromExpression(() => aggregates).
                FirstOrDefaultAsync(x => x.TenantId.Equals(tenantId) && x.Id.Equals(id));

            var filteredResult = _tenantRepositoryFilter.FilterResult(result);

            if (filteredResult == null)
            {
                throw new KeyNotFoundException($"No aggregate root could be found with specified identity {nameof(tenantId)}, {nameof(id)}");
            }

            return filteredResult;
        }

        public virtual async Task<List<TMultiTenantAggregateRootBase>> GetAllAsync(TTenantIdentity tenantId)
        {
            var aggregates = _context.Set<TMultiTenantAggregateRootBase>();

            var result = await _context.FromExpression(() => aggregates).
                Where(x => x.TenantId.Equals(tenantId)).ToListAsync();

            return result;
        }

        public virtual async Task UpdateAsync(TMultiTenantAggregateRootBase aggregateRoot)
        {
            _tenantRepositoryFilter.FilterUpdated(aggregateRoot);

            await Task.FromResult(_context.Update(aggregateRoot));
        }
    }
}
