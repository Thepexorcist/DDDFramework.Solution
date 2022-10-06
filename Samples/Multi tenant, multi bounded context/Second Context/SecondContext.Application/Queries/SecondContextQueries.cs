using Domain.Infrastructure.Queries.Interfaces;
using Domain.Infrastructure.Tenancy.Interfaces;
using SecondContext.Application.Queries.Interfaces;
using SecondContext.Application.Queries.ReadModels;
using SecondContext.Infrastructure.Persistance;

namespace SecondContext.Application.Queries
{
    public class SecondContextQueries : ISecondContextQueries
    {
        #region Fields

        private readonly IConnection<SecondContextDbContext> _connection;
        private readonly ITenantQueryFilter<int> _tenantQueryFilter;

        #endregion

        public SecondContextQueries(IConnection<SecondContextDbContext> connection, ITenantQueryFilter<int> tenantQueryFilter)
        {
            _connection = connection;
            _tenantQueryFilter = tenantQueryFilter;
        }

        public async Task<ProjectOverviewReadModel> GetProjectOverviewAsync(int tenantId)
        {
            var projectOverviewReadModel = new ProjectOverviewReadModel();
            projectOverviewReadModel.TenantId = tenantId;

            var result = await _connection.Query<ProjectForListReadModel>(
                @"SELECT
                    p.Id AS 'ProjectId',
                    p.ProjectNumber,
                    p.DisplayName,
                    p.IsPublished
                FROM 
                    [Project] p 
                WHERE 
                    p.TenantId = @tenantId", new { tenantId });

            projectOverviewReadModel.Projects = result.ToList();
            return projectOverviewReadModel;
        }

        public async Task<ProjectReadModel> GetProjectAsync(int tenantId, int projectId)
        {
            var result = await _connection.Query<ProjectReadModel>(
                @"SELECT
                    p.TenantId,
                    p.Id AS 'ProjectId',
                    p.ProjectNumber,
                    p.DisplayName
                FROM 
                    [Project] p 
                WHERE 
                    p.TenantId = @tenantId
                        AND
                    p.Id = @projectId", new { tenantId, projectId });

            if (result.Count() == 0)
            {
                return new ProjectReadModel();
            }

            var filteredResult = _tenantQueryFilter.FilterResult(result.First());

            var projectManager = await _connection.Query<ProjectManagerReadModel>(
                @"SELECT
                    papm.EmployeeNumber, 
                    papm.FirstName, 
                    papm.LastName 
                FROM
                    [ProjectAssignedProjectManager] papm 
                WHERE
                    papm.ProjectId = @projectId", new { projectId });

            if (projectManager.Count() > 0)
            {
                filteredResult.ProjectManager = projectManager.First();
            }

            var electricians = await _connection.Query<ElectricianReadModel>(
                @"SELECT
                    pae.EmployeeNumber, 
                    pae.FirstName, 
                    pae.LastName 
                FROM
                    [ProjectAssignedElectrician] pae 
                WHERE
                    pae.ProjectId = @projectId", new { projectId });

            if (electricians.Count() > 0)
            {
                filteredResult.Electricians = electricians.ToList();
            }

            var documents = await _connection.Query<DocumentReadModel>(
                @"SELECT
                    pd.Id AS 'DocumentId',
                    pd.Name,
                    pd.Version 
                FROM 
                    [ProjectDocument] pd 
                WHERE 
                    pd.ProjectId = @projectId", new { projectId });


            filteredResult.Documents = documents.ToList();

            foreach (var document in filteredResult.Documents)
            {
                var revisions = await _connection.Query<RevisionReadModel>(
                    @"SELECT
                        pdr.Comment,
                        pdr.Version
                    FROM 
                        [ProjectDocumentRevision] pdr
                    WHERE 
                        pdr.DocumentId = @documentId", new { documentId = document.DocumentId });

                document.RevisionHistory = revisions.ToList();
            }
            
            return filteredResult;
        }
    }
}
