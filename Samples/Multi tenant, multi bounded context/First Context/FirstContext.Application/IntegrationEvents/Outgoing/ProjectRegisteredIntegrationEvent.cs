using EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.IntegrationEvents.Outgoing
{
    public class ProjectRegisteredIntegrationEvent : IntegrationEvent
    {
        public int TenantId { get; }
        public int WorkspaceId { get; }
        public string UniqueProjectNumber { get; }
        public string ProjectName { get; }  

        public ProjectRegisteredIntegrationEvent(Guid id, int tenantId, int workspaceId, string uniqueProjectNumber, string projectName) : base(id)
        {
            TenantId = tenantId;
            WorkspaceId = workspaceId;
            UniqueProjectNumber = uniqueProjectNumber;
            ProjectName = projectName;
        }
    }
}
