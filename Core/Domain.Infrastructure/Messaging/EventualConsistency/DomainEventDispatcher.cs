using Autofac;
using Autofac.Core;
using Domain.DomainEvents;
using Domain.Entities.Interfaces;
using Domain.Infrastructure.Messaging.EventualConsistency.Interfaces;
using Domain.Infrastructure.Messaging.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Infrastructure.Messaging.EventualConsistency
{
    /// <summary>
    /// Domain event dispatcher that will also use the outbox pattern for external notifications to be sent to external listeners.
    /// Every domain event that can be sent as notifications should implement the IDomainNotificationEvent<TDomainEvent>.
    /// These will be serialized into the Outbox message table where the OutboxMessageBackroundService polls the table
    /// for domain notifications not yet sent. This will ensure eventual consistency without using 2PC transactions.
    /// </summary>
    /// <typeparam name="TContext">The database context within the local transaction will occur.</typeparam>
    public class DomainEventDispatcher<TContext> : IDomainEventDispatcher<TContext> where TContext : DbContext
    {
        #region Fields

        private readonly ILifetimeScope _lifetimeScope;
        private readonly IMediator _mediator;

        #endregion

        #region Properties



        #endregion

        #region Constructors

        public DomainEventDispatcher(ILifetimeScope lifetimeScope, IMediator mediator)
        {
            _lifetimeScope = lifetimeScope;
            _mediator = mediator;
        }

        #endregion

        #region Public methods

        public async Task DispatchDomainEventsAsync(IAggregateRoot aggregateRoot, TContext context)
        {
            var domainEvents = aggregateRoot.DomainEvents;

            var tasks = domainEvents
                .Select(async (domainEvent) =>
                {
                    await _mediator.Publish(CreateWrappedDomainEvent((dynamic)domainEvent));
                });

            // All domain events have been published and part of the initial transaction.
            await Task.WhenAll(tasks);

            // Find all notifications that should be triggered by the domain events.
            // These will be saved to outbox table in the same database context using same transaction as the initial request.
            var domainEventNotifications = ResolveDomainEventNotifications(domainEvents);

            foreach (var domainEventNotification in domainEventNotifications)
            {
                string type = domainEventNotification.GetType().AssemblyQualifiedName;
                var data = JsonConvert.SerializeObject(domainEventNotification);
                var outboxMessage = new OutboxMessage(
                    domainEventNotification.DomainEvent.OccurredOn,
                    type,
                    data);
                context.Add(outboxMessage);
            }

            // Clear all domain events.
            aggregateRoot.ClearDomainEvents();
        }

        #endregion

        #region Private methods

        private List<IDomainEventNotification<DomainEventBase>> ResolveDomainEventNotifications(IReadOnlyCollection<DomainEventBase> domainEvents)
        {
            var domainEventNotifications = new List<IDomainEventNotification<DomainEventBase>>();
            foreach (var domainEvent in domainEvents)
            {
                Type domainEvenNotificationType = typeof(IDomainEventNotification<>);
                var domainNotificationWithGenericType = domainEvenNotificationType.MakeGenericType(domainEvent.GetType());
                var domainNotification = _lifetimeScope.ResolveOptional(domainNotificationWithGenericType, new List<Parameter>
                {
                    new NamedParameter("domainEvent", domainEvent)
                });

                if (domainNotification != null)
                {
                    domainEventNotifications.Add(domainNotification as IDomainEventNotification<DomainEventBase>);
                }
            }

            return domainEventNotifications;
        }

        private static WrappedDomainEvent<TDomainEvent> CreateWrappedDomainEvent<TDomainEvent>(TDomainEvent domainEvent) where TDomainEvent : DomainEventBase
        {
            return new WrappedDomainEvent<TDomainEvent>(domainEvent);
        }

        #endregion
    }
}
