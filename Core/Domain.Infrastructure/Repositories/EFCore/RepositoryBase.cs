using Domain.Entities;
using Domain.Repositories.Interfaces;
using Domain.Transactions.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Repositories.EFCore
{
    public abstract class RepositoryBase<TContext, TAggregateRootBase, TIdentity> :
          IRepository<TAggregateRootBase, TIdentity>
          where TContext : DbContext, IUnitOfWork
          where TAggregateRootBase : AggregateRootBase<TIdentity>
    {
        #region Fields

        protected TContext _context;

        #endregion

        #region Properties

        public IUnitOfWork UnitOfWork => _context;

        #endregion

        #region Constructors

        public RepositoryBase(TContext context)
        {
            _context = context;
        }

        #endregion

        #region Public methods

        public virtual async Task<TAggregateRootBase> GetAsync(TIdentity id)
        {
            var result = await _context.FindAsync<TAggregateRootBase>(id);

            if (result == null)
            {
                throw new KeyNotFoundException($"No aggregate root could be found with specified identity {nameof(id)}");
            }

            return result;
        }

        public virtual async Task AddAsync(TAggregateRootBase aggregateRoot)
        {
            await _context.AddAsync(aggregateRoot);
        }

        public virtual async Task UpdateAsync(TAggregateRootBase aggregateRoot)
        {
            await Task.FromResult(_context.Update(aggregateRoot));
        }

        #endregion
    }
}
