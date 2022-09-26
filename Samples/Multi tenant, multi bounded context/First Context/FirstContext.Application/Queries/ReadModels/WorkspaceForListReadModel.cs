using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.Queries.ReadModels
{
    public class WorkspaceForListReadModel
    {
        public int WorkspaceId { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
