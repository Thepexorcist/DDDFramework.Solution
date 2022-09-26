using FirstContext.Application.Queries.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.Queries.Interfaces
{
    public interface IFirstContextQueries
    {
        Task<IEnumerable<TenantForListReadModel>> GetAllTenantsAsync();
        Task<TenantReadModel> GetTenantAsync(int tenantId);
        Task<WorkspaceReadModel> GetWorkspaceAsync(int tenantId, int workspaceId);
    }
}
