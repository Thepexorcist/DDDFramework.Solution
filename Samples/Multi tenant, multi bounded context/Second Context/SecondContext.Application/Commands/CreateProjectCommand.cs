using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Application.Commands
{
    public class CreateProjectCommand : IRequest<bool>
    {
        public int TenantId { get; set; }
        public string ProjectNumber { get; set; }
        public string DisplayName { get; set; }
    }
}
