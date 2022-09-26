using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.Queries.ReadModels
{
    public class TenantReadModel
    {
        public int TenantId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public List<WorkspaceForListReadModel> Workspaces { get; set; }
        public TenantReadModel()
        {
            Workspaces = new List<WorkspaceForListReadModel>();
        }
    }
}
