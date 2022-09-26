using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Tenancy.Interfaces
{
    public interface ITenantOwned<TTenantIdentity>
    {
        public TTenantIdentity TenantId { get; }
    }
}
