using Domain.DomainEvents;
using FirstContext.Domain.Aggregates.TenantAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Domain.Aggregates.WorkspaceAggregate.DomainEvents
{
    public class ProjectPublished : DomainEventBase
    {
        public TenantId TenantId { get; }
        public WorkspaceId WorkspaceId { get; }
        public string UniqueProjectNumber { get; }
        public string ProjectName { get; }

        public ProjectPublished(TenantId tenantId, WorkspaceId workspaceId, string uniqueProjectNumber)
        {
            TenantId = tenantId;
            WorkspaceId = workspaceId;
            UniqueProjectNumber = uniqueProjectNumber;
        }
    }
}
