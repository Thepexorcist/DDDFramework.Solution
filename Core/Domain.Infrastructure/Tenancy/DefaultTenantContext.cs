using Domain.Infrastructure.Tenancy.Interfaces;
using Domain.Tenancy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Tenancy
{
    public class DefaultTenantContext<TTenantIdentity> : ITenantContext<TTenantIdentity>
    {
        public DefaultTenantContext(ITenantContextResolver<TTenantIdentity> tenantContextResolver)
        {
            var tenantOverride = TenantContextOverride<TTenantIdentity>.Current;
            if (tenantOverride != null)
            {
                Tenant = tenantOverride.Tenant;
            }
            else
            {
                Tenant = tenantContextResolver.ResolveTenant();
            }
        }

        public ITenant<TTenantIdentity> Tenant { get; }
    }
}
