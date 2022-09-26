using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Interfaces
{
    public interface ISubscriptionManager
    {
        void AddSubscription<TIntegrationEventHandler, TIntegrationEvent>()
            where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
            where TIntegrationEvent : IntegrationEvent;

        Type GetEventTypeByName(string eventName);

        string GetEventKey<TIntegrationEvent>();

        bool HasSubscriptionsForEvent(string eventName);

        bool IsEmpty();

        List<Type> GetIntegrationEventHandlersForEvent(string eventName);
    }
}
