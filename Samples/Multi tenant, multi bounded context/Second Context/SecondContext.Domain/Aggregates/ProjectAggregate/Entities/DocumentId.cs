using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Domain.Aggregates.ProjectAggregate.Entities
{
    public class DocumentId : IdentityBase<Guid>
    {
        public DocumentId(Guid id) : base(id)
        {
        }
    }
}
