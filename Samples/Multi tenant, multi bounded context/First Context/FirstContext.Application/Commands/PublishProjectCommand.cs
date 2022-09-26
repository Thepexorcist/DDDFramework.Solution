﻿using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.Commands
{
    public class PublishProjectCommand : IRequest<bool>
    {
        public int TenantId { get; set; }
        public int WorkspaceId { get; set; }
        public int ProjectId { get; set; }
    }
}
