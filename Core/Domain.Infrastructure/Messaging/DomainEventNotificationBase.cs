using Domain.DomainEvents;
using Domain.Infrastructure.Messaging.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Messaging
{
    public abstract class DomainEventNotificationBase<TDomainEvent> :
        IDomainEventNotification<TDomainEvent>
        where TDomainEvent : DomainEventBase
    {
        [JsonIgnore]
        public TDomainEvent DomainEvent { get; }

        public Guid Id { get; set; }

        public DomainEventNotificationBase(TDomainEvent domainEvent)
        {
            Id = Guid.NewGuid();
            DomainEvent = domainEvent;
        }
    }
}
