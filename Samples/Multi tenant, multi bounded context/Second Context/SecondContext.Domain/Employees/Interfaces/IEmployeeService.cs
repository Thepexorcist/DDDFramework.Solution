using SecondContext.Domain.Tenant;

namespace SecondContext.Domain.Employees.Interfaces
{
    /// <summary>
    /// Domain service.
    /// </summary>
    public interface IEmployeeService
    {
        Task<ProjectManager> FindProjectManagerByEmployeeNumberAsync(TenantId tenantId, int employeeNumber);
        Task<Electrician> FindElectricianByEmployeeNumberAsync(TenantId tenantId, int employeeNumber);
    }
}
