using Domain.Infrastructure.Queries.Interfaces;
using FirstContext.Application.Queries.Interfaces;
using FirstContext.Application.Queries.ReadModels;
using FirstContext.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.Queries
{
    public class FirstContextQueries : IFirstContextQueries
    {
        #region Fields

        private readonly IConnection<FirstContextDbContext> _connection;

        #endregion

        #region Constructors

        public FirstContextQueries(IConnection<FirstContextDbContext> connection)
        {
            _connection = connection;
        }

        #endregion

        public async Task<IEnumerable<TenantForListReadModel>> GetAllTenantsAsync()
        {
            var result = await _connection.Query<TenantForListReadModel>(
                @"SELECT
                    t.Id AS 'TenantId',
                    t.Name,
                    t.IsActive 
                FROM 
                    [Tenant] t");

            return result.ToList();
        }

            public async Task<TenantReadModel> GetTenantAsync(int tenantId)
        {
            var result = await _connection.Query<TenantReadModel>(
                @"SELECT
                    t.Id AS 'TenantId',
                    t.Name,
                    t.IsActive 
                FROM 
                    [Tenant] t 
                WHERE 
                    t.Id = @tenantId", new { tenantId });

            if (result.Count() == 0)
            {
                return new TenantReadModel();
            }

            var workspaces = await _connection.Query<WorkspaceForListReadModel>(
                @"SELECT 
                    w.Id AS 'WorkspaceId',
                    w.Name,
                    w.IsActive 
                FROM 
                    [Workspace] w 
                WHERE  
                    w.TenantId = @tenantId", new { tenantId });

            var readModel = result.First();
            readModel.Workspaces = workspaces.ToList();
            return readModel;
        }

        public async Task<WorkspaceReadModel> GetWorkspaceAsync(int tenantId, int workspaceId)
        {
            var result = await _connection.Query<WorkspaceReadModel>(
                @"SELECT
                    w.TenantId,
                    w.Id AS 'WorkspaceId',
                    w.IsActive 
                FROM 
                    [Workspace] w 
                WHERE 
                    w.TenantId = @tenantId 
                        AND 
                    w.Id = @workspaceId", new { tenantId, workspaceId });

            if (result.Count() == 0)
            {
                return new WorkspaceReadModel();
            }

            var projects = await _connection.Query<ProjectReadModel>(
                @"SELECT
                    wp.Id AS 'ProjectId',
                    wp.UniqueProjectNumber,
                    wp.Name,
                    wp.Created,
                    wp.IsPublished
                FROM 
                    [WorkspaceProject] wp
                WHERE 
                    wp.WorkspaceId = @workspaceId", new { workspaceId });

            var logEntries = await _connection.Query<LogEntryReadModel>(
                @"SELECT
                    wle.Created,
                    wle.Action
                FROM 
                    [WorkspaceLogEntry] wle
                WHERE 
                    wle.WorkspaceId = @workspaceId", new { workspaceId });

            var readModel = result.First();
            readModel.Projects = projects.ToList();
            readModel.LogEntries = logEntries.ToList();
            return readModel;
        }
    }
}
