using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Application.Queries.ReadModels
{
    public class DocumentReadModel
    {
        public Guid DocumentId { get; set; }
        public string Name { get; set; }
        public int Version { get; set; }
        public List<RevisionReadModel> RevisionHistory { get; set; }
        public DocumentReadModel()
        {
            RevisionHistory = new List<RevisionReadModel>();
        }
    }
}
