using Domain.Tenancy.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.Queries.ReadModels
{
    public class WorkspaceReadModel : ITenantOwned<int>
    {
        public int TenantId { get; set; }
        public int WorkspaceId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<ProjectReadModel> Projects { get; set; }
        public List<LogEntryReadModel> LogEntries { get; set; }
        public WorkspaceReadModel()
        {
            Projects = new List<ProjectReadModel>();
            LogEntries = new List<LogEntryReadModel>();
        }
    }
}
