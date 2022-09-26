using Domain.ValueObjects;

namespace FirstContext.Domain.Aggregates.WorkspaceAggregate.Entities
{
    public class ProjectId : IdentityBase<int>
    {
        public ProjectId(int id) : base(id)
        {
        }
    }
}
