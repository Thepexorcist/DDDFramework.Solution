using Domain.Infrastructure.Queries.Interfaces;
using SecondContext.Application.Queries.Interfaces;
using SecondContext.Application.Queries.ReadModels;
using SecondContext.Infrastructure.Persistance;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Application.Queries
{
    public class SecondContextQueries : ISecondContextQueries
    {
        #region Fields

        private readonly IConnection<SecondContextDbContext> _connection;

        #endregion

        public SecondContextQueries(IConnection<SecondContextDbContext> connection)
        {
            _connection = connection;
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
            var project = result.First();

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
                project.ProjectManager = projectManager.First();
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
                project.Electricians = electricians.ToList();
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

            
            project.Documents = documents.ToList();

            foreach (var document in project.Documents)
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
            
            return project;
        }
    }
}
