using Domain.Tenancy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Tenancy.Interfaces
{
    public interface ITenantRepositoryFilter<TTenantIdentity>
    {
        TMultiTenantAggregateRoot FilterResult<TMultiTenantAggregateRoot>(TMultiTenantAggregateRoot aggregateRoot) where TMultiTenantAggregateRoot : ITenantOwned<TTenantIdentity>;
        void FilterAdded<TMultiTenantAggregateRoot>(TMultiTenantAggregateRoot aggregateRoot) where TMultiTenantAggregateRoot : ITenantOwned<TTenantIdentity>;
        void FilterUpdated<TMultiTenantAggregateRoot>(TMultiTenantAggregateRoot aggregateRoot) where TMultiTenantAggregateRoot : ITenantOwned<TTenantIdentity>;
        void FilterDeleted<TMultiTenantAggregateRoot>(TMultiTenantAggregateRoot aggregateRoot) where TMultiTenantAggregateRoot : ITenantOwned<TTenantIdentity>;
    }
}
