using Domain.ValueObjects;

namespace FirstContext.Domain.Aggregates.WorkspaceAggregate.ValueObjects
{
    public sealed class LogEntry : ValueObjectBase
    {
        public DateTime Created { get; }
        public string Action { get; }

        public LogEntry(string action)
        {
            Created = DateTime.Now;
            Action = action;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Created;
            yield return Action;
        }
    }
}
