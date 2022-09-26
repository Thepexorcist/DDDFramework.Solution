using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Domain.Employees
{
    public abstract class Employee : ValueObjectBase
    {
        public int EmployeeNumber { get; }
        public string FirstName { get; }
        public string LastName { get; }

        public Employee(int employeeNumber, string firstName, string lastName)
        {
            EmployeeNumber = employeeNumber;
            FirstName = firstName;
            LastName = lastName;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return EmployeeNumber;
            yield return FirstName;
            yield return LastName;
        }
    }
}
