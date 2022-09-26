using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Application.Queries.ReadModels
{
    public class ProjectManagerReadModel
    {
        public int EmployeeNumber { get; set; } = -1;
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
