using Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecondContext.Domain.Tenant
{
    public class TenantId : IdentityBase<int>
    {
        public TenantId(int id) : base(id)
        {
        }
    }
}
