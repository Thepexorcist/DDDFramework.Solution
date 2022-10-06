using Domain.Infrastructure.Tenancy.Interfaces;
using Domain.Tenancy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Tenancy
{
    public class TenantRepositoryFilter<TTenantIdentity> : ITenantRepositoryFilter<TTenantIdentity>
    {
        private readonly Lazy<ITenantContext<TTenantIdentity>> _tenantContext;

        public TenantRepositoryFilter(Lazy<ITenantContext<TTenantIdentity>> tenantContext)
        {
            _tenantContext = tenantContext;
        }

        public void FilterAdded<TMultiTenantAggregateRoot>(TMultiTenantAggregateRoot aggregateRoot) where TMultiTenantAggregateRoot : ITenantOwned<TTenantIdentity>
        {
            var tenant = _tenantContext.Value.Tenant;

            if (!tenant.TenantId.Equals(aggregateRoot.TenantId))
            {
                throw new InvalidOperationException("Forbidden to add data to another tenant");
            }
        }

        public void FilterDeleted<TMultiTenantAggregateRoot>(TMultiTenantAggregateRoot aggregateRoot) where TMultiTenantAggregateRoot : ITenantOwned<TTenantIdentity>
        {
            var tenant = _tenantContext.Value.Tenant;

            if (!tenant.TenantId.Equals(aggregateRoot.TenantId))
            {
                throw new InvalidOperationException("Forbidden to delete data of another tenant");
            }
        }

        public TMultiTenantAggregateRoot FilterResult<TMultiTenantAggregateRoot>(TMultiTenantAggregateRoot result) where TMultiTenantAggregateRoot : ITenantOwned<TTenantIdentity>
        {
            var tenant = _tenantContext.Value.Tenant;

            if (!tenant.TenantId.Equals(result.TenantId))
            {
                throw new InvalidOperationException("Forbidden to query data of another tenant");
            }

            return result;
        }

        public void FilterUpdated<TMultiTenantAggregateRoot>(TMultiTenantAggregateRoot aggregateRoot) where TMultiTenantAggregateRoot : ITenantOwned<TTenantIdentity>
        {
            var tenant = _tenantContext.Value.Tenant;

            if (!tenant.TenantId.Equals(aggregateRoot.TenantId))
            {
                throw new InvalidOperationException("Forbidden to update data of another tenant");
            }
        }
    }
}
