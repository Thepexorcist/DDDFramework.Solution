using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Application.Queries.ReadModels
{
    public class ProjectOverviewReadModel
    {
        public int TenantId { get; set; }
        public List<ProjectForListReadModel> Projects { get; set; }
        public ProjectOverviewReadModel()
        {
            Projects = new List<ProjectForListReadModel>();
        }
    }
}
