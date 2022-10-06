using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tenancy.Interfaces
{
    /// <summary>
    /// Interface defining which identity a tenant has.
    /// </summary>
    /// <typeparam name="TTenantIdentity">The identity type for the tenant.</typeparam>
    public interface ITenant<TTenantIdentity>
    {
        public TTenantIdentity TenantId { get; }
    }
}
