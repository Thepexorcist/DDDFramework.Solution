using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Application.Queries.ReadModels
{
    public class RevisionReadModel
    {
        public string Comment { get; set; }
        public int Version { get; set; }
    }
}
