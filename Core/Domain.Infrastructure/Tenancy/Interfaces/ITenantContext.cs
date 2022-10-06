using Domain.Tenancy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Tenancy.Interfaces
{
    /// <summary>
    /// Provides the current tenant for the context.
    /// </summary>
    public interface ITenantContext<TTenantIdentity>
    {
        public ITenant<TTenantIdentity> Tenant { get; }
    }
}
