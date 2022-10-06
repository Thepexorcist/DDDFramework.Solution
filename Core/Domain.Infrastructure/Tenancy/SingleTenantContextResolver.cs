using Domain.Infrastructure.Tenancy.Interfaces;
using Domain.Tenancy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Tenancy
{
    public class SingleTenantContextResolver<TTenantIdentity> : ITenantContextResolver<TTenantIdentity>
    {
        private readonly ITenantProvider<TTenantIdentity> _tenantProvider;

        public SingleTenantContextResolver(ITenantProvider<TTenantIdentity> tenantProvider)
        {
            _tenantProvider = tenantProvider;
        }

        public ITenant<TTenantIdentity> ResolveTenant()
        {
            return _tenantProvider.GetTenant();
        }
    }
}
