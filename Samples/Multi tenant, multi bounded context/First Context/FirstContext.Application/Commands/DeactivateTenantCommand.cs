using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.Commands
{
    public class DeactivateTenantCommand : IRequest<bool>
    {
        public int TenantId { get; set; }
    }
}
