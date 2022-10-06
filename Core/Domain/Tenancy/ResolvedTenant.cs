using Domain.Tenancy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tenancy
{
    public sealed class ResolvedTenant<TTenantIdentity> : ITenant<TTenantIdentity>
    {
        public TTenantIdentity TenantId { get; }

        public ResolvedTenant(TTenantIdentity tenantId)
        {
            TenantId = tenantId;
        }
    }
}
