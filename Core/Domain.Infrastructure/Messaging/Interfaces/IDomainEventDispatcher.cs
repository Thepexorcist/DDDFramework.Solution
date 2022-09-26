using Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Messaging.Interfaces
{
    public interface IDomainEventDispatcher<TContext>
    {
        Task DispatchDomainEventsAsync(IAggregateRoot aggregateRoot, TContext context);
    }
}
