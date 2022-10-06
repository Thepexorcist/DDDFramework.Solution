using Microsoft.EntityFrameworkCore;
using FirstContext.Domain.Aggregates.TenantAggregate;
using FirstContext.Domain.Aggregates.WorkspaceAggregate;
using FirstContext.Domain.Aggregates.WorkspaceAggregate.Entities;
using Domain.Infrastructure.Repositories.EFCore;
using FirstContext.Domain.Aggregates.WorkspaceAggregate.Repositories.Interfaces;
using Domain.Tenancy.Repositories.Interfaces;
using Domain.Infrastructure.Tenancy.Interfaces;

namespace FirstContext.Infrastructure.Persistance
{
    public class WorkspaceRepository : 
        MultiTenantRepositoryBase<FirstContextDbContext, Workspace, TenantId, WorkspaceId>, 
        IWorkspaceRepository
    {
        public WorkspaceRepository(FirstContextDbContext context, ITenantRepositoryFilter<TenantId> tenantRepositoryFilter) : base(context, tenantRepositoryFilter)
        {
        }

        public async Task<WorkspaceId> GetNextIdentityAsync()
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT NEXT VALUE FOR [dbo].[WorkspaceSequence];";

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                {
                    await command.Connection.OpenAsync();
                }

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var id = reader.GetInt32(0);
                        return new WorkspaceId(id);
                    }
                }
            }

            return new WorkspaceId(default);
        }

        public override async Task UpdateAsync(Workspace aggregateRoot)
        {
            var projectId = -1;
            foreach(var project in aggregateRoot.Projects)
            {
                if (project.Id == null || project.Id.Value == 0)
                {
                    project.Id = new ProjectId(projectId);
                    projectId--;

                    _context.Add(project);
                }
            }

            await base.UpdateAsync(aggregateRoot);
        }

        public override async Task<Workspace> GetAsync(TenantId tenantId, WorkspaceId id)
        {
            var workspace = await base.GetAsync(tenantId, id);

            await _context.Entry(workspace)
                   .Collection(i => i.Projects).LoadAsync();

            return workspace;
        }

        public override async Task<List<Workspace>> GetAllAsync(TenantId tenantId)
        {
            var workspaces = await base.GetAllAsync(tenantId);

            foreach(var workspace in workspaces)
            {
                await _context.Entry(workspace)
                   .Collection(i => i.Projects).LoadAsync();
            }

            return workspaces;
        }
    }
}
