using Domain.Entities;
using FirstContext.Domain.Aggregates.TenantAggregate.DomainEvents;
using FirstContext.Domain.Aggregates.WorkspaceAggregate;

namespace FirstContext.Domain.Aggregates.TenantAggregate
{
    public class Tenant : AggregateRootBase<TenantId>
    {
        public string Name { get; }
        public bool IsActive { get; private set; }
        protected Tenant() { }

        public Tenant(TenantId id, string name) : base(id)
        {
            Name = name;

            Activate();
        }

        /// <summary>
        /// Creates a new workspace. 
        /// This was we will always ensure that creation of workspace is within the tenant
        /// and all invariants in the tenant are ok before we create the workspace.
        /// </summary>
        /// <param name="workspaceId"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public Workspace RegisterWorkspace(WorkspaceId workspaceId, string name)
        {
            if (!IsActive)
            {
                throw new FirstContextDomainException("Can not register a new workspace on a deactivated tenant.");
            }

            var workspace = new Workspace(Id, workspaceId, name);
            return workspace;
        }

        public void Activate()
        {
            if (IsActive)
            {
                return;
            }

            IsActive = true;
            AddDomainEvent(new TenantActivated(this));
        }

        public void Deactivate()
        {
            if (!IsActive)
            {
                return;
            }

            IsActive = false;
            AddDomainEvent(new TenantDeactivated(this));
        }
    }
}
