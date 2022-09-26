using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Application.Commands
{
    public class AssignElectricianCommand : IRequest<bool>
    {
        public int TenantId { get; set; }
        public int ProjectId { get; set; }
        public int EmployeeNumber { get; set; }
    }
}
