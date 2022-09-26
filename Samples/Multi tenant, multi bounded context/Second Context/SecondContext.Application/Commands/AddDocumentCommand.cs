using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Application.Commands
{
    public class AddDocumentCommand : IRequest<bool>
    {
        public int TenantId { get; set; }
        public int ProjectId { get; set; }
        public Guid DocumentId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string URL { get; set; }
    }
}
