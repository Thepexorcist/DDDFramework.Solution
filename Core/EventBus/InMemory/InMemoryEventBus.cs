using Autofac;
using EventBus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EventBus.InMemory
{
    public sealed class InMemoryEventBus
    {
        public static InMemoryEventBus Instance { get; } = new InMemoryEventBus();
        private readonly ISubscriptionManager _subscriptionManager = new SubscriptionManager();
        private readonly IDictionary<string, List<IIntegrationEventHandler>> _handlersDictionary;

        static InMemoryEventBus()
        {
        }

        private InMemoryEventBus()
        {
            _subscriptionManager = new SubscriptionManager();
            _handlersDictionary = new Dictionary<string, List<IIntegrationEventHandler>>();
        }

        public void Subscribe<TIntegrationEvent, TIntegrationEventHandler>()
            where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
            where TIntegrationEvent : IntegrationEvent
        {
            _subscriptionManager.AddSubscription<IIntegrationEventHandler<TIntegrationEvent>, TIntegrationEvent>();
        }

        public async Task Publish(ILifetimeScope lifetimeScope, IntegrationEvent integrationEvent)
        {
            var eventName = integrationEvent.GetType().Name;

            // Go to next iteration if no handler registered for the event type.
            if (!_subscriptionManager.HasSubscriptionsForEvent(eventName))
            {
                return;
            }

            using (var scope = lifetimeScope.BeginLifetimeScope("IN_MEMORY_EVENT_BUS"))
            {
                var integrationEventHandlers = _subscriptionManager.GetIntegrationEventHandlersForEvent(eventName);
                var eventType = _subscriptionManager.GetEventTypeByName(eventName);

                foreach (var integrationEventHandler in integrationEventHandlers)
                {
                    var handler = scope.ResolveOptional(integrationEventHandler);
                    if (handler == null)
                    {
                        continue;
                    }

                    // Serialize the original integration event.
                    // This will later be deserialized to fit the new handler.
                    var content = JsonSerializer.Serialize(integrationEvent, integrationEvent.GetType(), new JsonSerializerOptions
                    {
                        WriteIndented = true
                    });

                    var newContextIntegrationEvent = JsonSerializer.Deserialize(content, eventType, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                    await Task.Yield();
                    await (Task)concreteType.GetMethod("Handle").Invoke(handler, new object[] { newContextIntegrationEvent });
                }
            }
        }
    }
}
