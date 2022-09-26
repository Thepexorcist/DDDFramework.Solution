using Autofac;
using EventBus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.InMemory
{
    public class InMemoryEventBusClient : IEventBus
    {
        private readonly ILifetimeScope _scope;

        public InMemoryEventBusClient(ILifetimeScope _lifetimeScope)
        {
            _scope = _lifetimeScope;
        }

        public void Dispose()
        {
        }

        public async Task Publish(IntegrationEvent integrationEvent)
        {
            await InMemoryEventBus.Instance.Publish(_scope, integrationEvent);
        }

        public void Subscribe<TIntegrationEvent, TIntegrationEventHandler>()
            where TIntegrationEvent : IntegrationEvent
            where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            InMemoryEventBus.Instance.Subscribe<TIntegrationEvent, TIntegrationEventHandler>();
        }
    }
}
