using Domain.Entities;
using Domain.Tenancy.Interfaces;

namespace Domain.Tenancy
{
    /// <summary>
    /// This is the aggregate root base for an aggregate that is owned by a specific tenant. 
    /// All multi tenant aggregates should inherit from this class.
    /// </summary>
    /// <typeparam name="TTenantIdentity">The tenant identity.</typeparam>
    /// <typeparam name="TIdentity">The identity of the aggregate.</typeparam>
    public abstract class MultiTenantAggregateRootBase<TTenantIdentity, TIdentity> : AggregateRootBase<TIdentity>, ITenantOwned<TTenantIdentity>
    {
        #region Fields



        #endregion

        #region Properties

        public TTenantIdentity TenantId { get; }

        #endregion

        #region Constructors

        protected MultiTenantAggregateRootBase() : base() { }

        protected MultiTenantAggregateRootBase(TTenantIdentity tenantId, TIdentity id) : base(id)
        {
            TenantId = tenantId;
        }

        #endregion

    }
}
