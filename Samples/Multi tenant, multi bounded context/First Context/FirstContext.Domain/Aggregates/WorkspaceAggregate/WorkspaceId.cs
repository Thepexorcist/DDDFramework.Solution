using Domain.ValueObjects;

namespace FirstContext.Domain.Aggregates.WorkspaceAggregate
{
    public class WorkspaceId : IdentityBase<int>
    {
        public WorkspaceId(int id) : base(id)
        {
        }
    }
}
