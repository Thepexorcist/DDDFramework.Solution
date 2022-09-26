using SecondContext.Domain.Employees;
using SecondContext.Domain.Employees.Interfaces;
using SecondContext.Domain.Tenant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Infrastructure.Employees.Mock
{
    public class EmployeeService : IEmployeeService
    {
        public async Task<Electrician> FindElectricianByEmployeeNumberAsync(TenantId tenantId, int employeeNumber)
        {
            var electricianOne = new Electrician(123, "Johan", "Johansson");
            var electricianTwo = new Electrician(124, "Patrik", "Eriksson");
            var electricianThree = new Electrician(125, "Sven", "Svensson");
            var electricianFour = new Electrician(126, "Dennis", "Menace");
            var electricianFive = new Electrician(127, "John", "Gacy");

            var electricians = new List<Electrician>();
            electricians.Add(electricianOne);
            electricians.Add(electricianTwo);
            electricians.Add(electricianThree);
            electricians.Add(electricianFour);
            electricians.Add(electricianFive);
            
            var electrician = electricians.FirstOrDefault(x => x.EmployeeNumber == employeeNumber);

            if (electrician == null)
            {
                return new Electrician(-1, "", "");
            }

            return await Task.FromResult(electrician);
        }

        public async Task<ProjectManager> FindProjectManagerByEmployeeNumberAsync(TenantId tenantId, int employeeNumber)
        {
            var projectManagerOne = new ProjectManager(23, "Wilhelm", "The Third");
            var projectManagerTwo = new ProjectManager(24, "Jonas", "Wilkinsson");
            var projectManagerThree = new ProjectManager(25, "Pappenheim", "Svensson");
            var projectManagerFour = new ProjectManager(26, "Winnie", "Jensson");
            var projectManagerFive = new ProjectManager(27, "Leonardo", "Da Vinci");

            var projectManagers = new List<ProjectManager>();
            projectManagers.Add(projectManagerOne);
            projectManagers.Add(projectManagerTwo);
            projectManagers.Add(projectManagerThree);
            projectManagers.Add(projectManagerFour);
            projectManagers.Add(projectManagerFive);

            var projectManager = projectManagers.FirstOrDefault(x => x.EmployeeNumber == employeeNumber);

            if (projectManager == null)
            {
                return new ProjectManager(-1, "", "");
            }

            return await Task.FromResult(projectManager);
        }
    }
}
