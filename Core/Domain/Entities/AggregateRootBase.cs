using Domain.DomainEvents;
using Domain.Entities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    /// <summary>
    /// This is the aggregate root base. All aggregates should inherit from this class.
    /// </summary>
    /// <typeparam name="TIdentity">The identity of the aggregate.</typeparam>
    public abstract class AggregateRootBase<TIdentity> : EntityBase<TIdentity>, IAggregateRoot<TIdentity>
    {
        #region Fields

        private readonly List<DomainEventBase> _domainEvents;

        #endregion

        #region Properties

        public IReadOnlyCollection<DomainEventBase> DomainEvents => _domainEvents;

        #endregion

        #region Constructors

        protected AggregateRootBase() : base() 
        { 
            _domainEvents = new List<DomainEventBase>();
        }

        protected AggregateRootBase(TIdentity id) : base(id) 
        {
            _domainEvents = new List<DomainEventBase>();
        }

        #endregion

        /// <summary>
        /// Clears all domain events stored up in the aggregate root.
        /// This will be called after the aggregate roots has been saved and the domain events dispatched.
        /// </summary>
        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }

        /// <summary>
        /// Adds a domain event to the collection of all buffered upp domain events.
        /// </summary>
        /// <param name="domainEvent">The domain event.</param>
        protected void AddDomainEvent(DomainEventBase domainEvent)
        {
            _domainEvents.Add(domainEvent); 
        }
    }
}
