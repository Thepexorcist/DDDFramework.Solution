using Domain.Tenancy.Repositories.Interfaces;
using FirstContext.Domain.Aggregates.TenantAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Domain.Aggregates.WorkspaceAggregate.Repositories.Interfaces
{
    public interface IWorkspaceRepository : IMultiTenantRepository<Workspace, TenantId, WorkspaceId>
    {
        Task<WorkspaceId> GetNextIdentityAsync();
    }
}
