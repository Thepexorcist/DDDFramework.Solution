using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Domain.Employees
{
    public sealed class Electrician : Employee
    {
        public Electrician(int employeeNumber, string firstName, string lastName) : base(employeeNumber, firstName, lastName)
        {
        }
    }
}
