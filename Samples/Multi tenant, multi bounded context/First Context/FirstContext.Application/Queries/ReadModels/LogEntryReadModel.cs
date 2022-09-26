using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.Queries.ReadModels
{
    public class LogEntryReadModel
    {
        public DateTime Created { get; set; }
        public string Action { get; set; }
    }
}
