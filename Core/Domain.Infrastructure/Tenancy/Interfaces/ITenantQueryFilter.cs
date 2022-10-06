using Domain.Tenancy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Tenancy.Interfaces
{
    public interface ITenantQueryFilter<TTenantIdentity>
    {
        TResult FilterResult<TResult>(TResult result) where TResult : ITenantOwned<TTenantIdentity>;
    }
}
