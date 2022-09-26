using EventBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Application.IntegrationEvents.Incoming
{
    public class ProjectPublishedIntegrationEvent : IntegrationEvent
    {
        public int TenantId { get; }
        public int WorkspaceId { get; }
        public string UniqueProjectNumber { get; }
        public string ProjectName { get; }

        public ProjectPublishedIntegrationEvent(Guid id, int tenantId, int workspaceId, string uniqueProjectNumber, string projectName) : base(id)
        {
            TenantId = tenantId;
            WorkspaceId = workspaceId;
            UniqueProjectNumber = uniqueProjectNumber;
            ProjectName = projectName;
        }
    }
}
