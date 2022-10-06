using Domain.Infrastructure.Tenancy.Interfaces;
using Domain.Tenancy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Tenancy
{
    public class DefaultTenantProvider<TTenantIdentity> : ITenantProvider<TTenantIdentity>
    {
        public ITenant<TTenantIdentity> GetTenant()
        {
            return null;
        }
    }
}
