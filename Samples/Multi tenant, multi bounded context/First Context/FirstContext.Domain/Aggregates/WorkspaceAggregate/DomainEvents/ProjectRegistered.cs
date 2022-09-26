using Domain.DomainEvents;
using FirstContext.Domain.Aggregates.TenantAggregate;

namespace FirstContext.Domain.Aggregates.WorkspaceAggregate.DomainEvents
{
    public class ProjectRegistered : DomainEventBase
    {
        public TenantId TenantId { get; }
        public WorkspaceId WorkspaceId { get; }
        public string UniqueProjectNumber { get; }
        public string ProjectName { get; }

        public ProjectRegistered(TenantId tenantId, WorkspaceId workspaceId, string uniqueProjectNumber, string projectName)
        {
            TenantId = tenantId;
            WorkspaceId = workspaceId;
            UniqueProjectNumber = uniqueProjectNumber;
            ProjectName = projectName;
        }
    }
}
