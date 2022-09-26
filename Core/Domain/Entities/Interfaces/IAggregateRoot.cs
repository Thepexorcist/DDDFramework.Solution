using Domain.DomainEvents;
using Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Interfaces
{
    public interface IAggregateRoot
    {
        IReadOnlyCollection<DomainEventBase> DomainEvents { get; }
        void ClearDomainEvents();
    }

    public interface IAggregateRoot<T> : IEntity<T>, IAggregateRoot
    {
    }
}
