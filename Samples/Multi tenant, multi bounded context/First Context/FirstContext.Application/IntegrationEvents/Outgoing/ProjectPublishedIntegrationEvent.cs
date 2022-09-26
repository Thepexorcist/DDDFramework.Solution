using EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Application.IntegrationEvents.Outgoing
{
    public class ProjectPublishedIntegrationEvent : IntegrationEvent
    {
        public int TenantId { get; }
        public int WorkspaceId { get; }
        public string UniqueProjectNumber { get; }

        public ProjectPublishedIntegrationEvent(Guid id, int tenantId, int workspaceId, string uniqueProjectNumber) : base(id)
        {
            TenantId = tenantId;
            WorkspaceId = workspaceId;
            UniqueProjectNumber = uniqueProjectNumber;
        }
    }
}
