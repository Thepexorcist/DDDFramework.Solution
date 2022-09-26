using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Application.Queries.ReadModels
{
    public class ProjectForListReadModel
    {
        public int ProjectId { get; set; }
        public string ProjectNumber { get; set; }
        public string DisplayName { get; set; }
        public bool IsPublished { get; set; }
    }
}
