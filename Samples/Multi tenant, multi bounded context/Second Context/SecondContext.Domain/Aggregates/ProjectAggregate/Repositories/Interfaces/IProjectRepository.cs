using Domain.Tenancy.Repositories.Interfaces;
using SecondContext.Domain.Tenant;

namespace SecondContext.Domain.Aggregates.ProjectAggregate.Repositories.Interfaces
{
    public interface IProjectRepository : IMultiTenantRepository<Project, TenantId, ProjectId>
    {
        Task<ProjectId> GetNextIdentityAsync();
        Task<Project> GetByProjectNumberAsync(TenantId tenantId, string projectNumber);
    }
}
