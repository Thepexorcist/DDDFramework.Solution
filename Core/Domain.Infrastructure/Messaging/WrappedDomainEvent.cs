using Domain.DomainEvents;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Messaging
{
    public class WrappedDomainEvent<TDomainEvent> : INotification where TDomainEvent : DomainEventBase
    {
        public TDomainEvent DomainEvent { get; private set; }

        public WrappedDomainEvent(TDomainEvent domainEvent)
        {
            DomainEvent = domainEvent;
        }
    }
}
