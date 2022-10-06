using Domain.Infrastructure.Tenancy.Interfaces;
using Domain.Tenancy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Tenancy
{
    public class TenantQueryFilter<TTenantIdentity> : ITenantQueryFilter<TTenantIdentity>
    {
        #region Fields

        private readonly Lazy<ITenantContext<TTenantIdentity>> _tenantContext;

        #endregion

        public TenantQueryFilter(Lazy<ITenantContext<TTenantIdentity>> tenantContext)
        {
            _tenantContext = tenantContext;
        }

        public TResult FilterResult<TResult>(TResult result) where TResult : ITenantOwned<TTenantIdentity>
        {
            var tenant = _tenantContext.Value.Tenant;

            if (!result.TenantId.Equals(tenant.TenantId))
            {
                throw new InvalidOperationException("Forbidden to query data of another tenant");
            }

            return result;
        }
    }
}
