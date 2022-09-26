using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Application.Queries.ReadModels
{
    public class ProjectReadModel
    {
        public int TenantId { get; set; }
        public int ProjectId { get; set; }
        public string ProjectNumber { get; set; }
        public string DisplayName { get; set; }
        public ProjectManagerReadModel ProjectManager { get; set; }
        public List<ElectricianReadModel> Electricians { get; set; }
        public List<DocumentReadModel> Documents { get; set; }
        public ProjectReadModel()
        {
            ProjectManager = new ProjectManagerReadModel();
            Electricians = new List<ElectricianReadModel>();
            Documents = new List<DocumentReadModel>();
        }
    }
}
