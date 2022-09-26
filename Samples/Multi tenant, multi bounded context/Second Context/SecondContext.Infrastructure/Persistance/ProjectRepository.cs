using Domain.Infrastructure.Repositories.EFCore;
using Microsoft.EntityFrameworkCore;
using SecondContext.Domain.Aggregates.ProjectAggregate;
using SecondContext.Domain.Aggregates.ProjectAggregate.Repositories.Interfaces;
using SecondContext.Domain.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Infrastructure.Persistance
{
    public class ProjectRepository :
        MultiTenantRepositoryBase<SecondContextDbContext, Project, TenantId, ProjectId>,
        IProjectRepository
    { 

        public ProjectRepository(SecondContextDbContext context) : base(context)
        {
        }

        public async override Task<Project> GetAsync(TenantId tenantId, ProjectId id)
        {
            var project = await base.GetAsync(tenantId, id);

            await _context.Entry(project)
                  .Collection(i => i.Documents).LoadAsync();

            return project;
        }

        public async Task<Project> GetByProjectNumberAsync(TenantId tenantId, string projectNumber)
        {
            var project = _context.Projects.SingleOrDefault(p => p.TenantId == tenantId && p.ProjectNumber == projectNumber);

            await _context.Entry(project)
                  .Collection(i => i.Documents).LoadAsync();

            return project;
        }

        public async Task<ProjectId> GetNextIdentityAsync()
        {
            using (var command = _context.Database.GetDbConnection().CreateCommand())
            {
                command.CommandText = "SELECT NEXT VALUE FOR [dbo].[ProjectSequence];";

                if (command.Connection.State == System.Data.ConnectionState.Closed)
                {
                    await command.Connection.OpenAsync();
                }

                using (var reader = await command.ExecuteReaderAsync())
                {
                    if (await reader.ReadAsync())
                    {
                        var id = reader.GetInt32(0);
                        return new ProjectId(id);
                    }
                }
            }

            return new ProjectId(default);
        }
    }
}
