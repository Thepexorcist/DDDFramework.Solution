using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Domain.Aggregates.ProjectAggregate
{
    public class ProjectId : IdentityBase<int>
    {
        public ProjectId(int id) : base(id)
        {
        }
    }
}
