using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.Queries.ReadModels
{
    public class ProjectReadModel
    {
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string UniqueProjectNumber { get; set; }
        public DateTime Created { get; set; }
        public bool IsPublished { get; set; }
    }
}
