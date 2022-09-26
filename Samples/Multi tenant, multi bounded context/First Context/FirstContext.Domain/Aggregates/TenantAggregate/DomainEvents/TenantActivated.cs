using Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Domain.Aggregates.TenantAggregate.DomainEvents
{
    public class TenantActivated : DomainEventBase
    {
        public Tenant Tenant { get; }

        public TenantActivated(Tenant tenant)
        {
            Tenant = tenant;
        }
    }
}
