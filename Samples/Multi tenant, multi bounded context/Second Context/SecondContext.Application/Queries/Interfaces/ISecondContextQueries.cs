using SecondContext.Application.Queries.ReadModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Application.Queries.Interfaces
{
    public interface ISecondContextQueries
    {
        Task<ProjectOverviewReadModel> GetProjectOverviewAsync(int tenantId);
        Task<ProjectReadModel> GetProjectAsync(int tenantId, int projectId);
    }
}
