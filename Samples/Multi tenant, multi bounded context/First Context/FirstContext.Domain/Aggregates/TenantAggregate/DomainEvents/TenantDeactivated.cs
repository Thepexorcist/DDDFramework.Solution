using Domain.DomainEvents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstContext.Domain.Aggregates.TenantAggregate.DomainEvents
{
    public class TenantDeactivated : DomainEventBase
    {
        public Tenant Tenant { get; }

        public TenantDeactivated(Tenant tenant)
        {
            Tenant = tenant;
        }
    }
}
