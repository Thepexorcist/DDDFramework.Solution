using Domain.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Domain.Aggregates.TenantAggregate.Repositories.Interfaces
{
    public interface ITenantRepository : IRepository<Tenant, TenantId>
    {
        Task<TenantId> GetNextIdentityAsync();
    }
}
