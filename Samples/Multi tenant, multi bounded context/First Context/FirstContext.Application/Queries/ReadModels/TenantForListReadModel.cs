using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.Queries.ReadModels
{
    public class TenantForListReadModel
    {
        public int TenantId { get; set; }
        public string Name { get; set; }
    }
}
