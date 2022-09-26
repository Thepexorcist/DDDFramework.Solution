namespace FirstContext.Domain.Aggregates.WorkspaceAggregate.DomainServices.Interfaces
{
    /// <summary>
    /// Domain service.
    /// Generates a unique number for a project.
    /// </summary>
    public interface IUniqueProjectNumberGenerator
    {
        string GenerateProjectNumber();
    }
}
