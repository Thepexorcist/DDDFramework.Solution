using EventBus.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus
{
    public class SubscriptionManager : ISubscriptionManager
    {
        #region Fields

        private readonly Dictionary<string, List<Type>> _integrationEventHandlers;
        private readonly List<Type> _eventTypes;

        #endregion

        #region Properties



        #endregion

        #region Constructors

        public SubscriptionManager()
        {
            _integrationEventHandlers = new Dictionary<string, List<Type>>();
            _eventTypes = new List<Type>();
        }

        #endregion

        #region Public methods

        public bool IsEmpty()
        {
            return _integrationEventHandlers.Count == 0;
        }

        public void AddSubscription<TIntegrationEventHandler, TIntegrationEvent>()
            where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
            where TIntegrationEvent : IntegrationEvent
        {
            var eventTypeName = GetEventKey<TIntegrationEvent>();

            if (!HasSubscriptionsForEvent(eventTypeName))
            {
                _integrationEventHandlers.Add(eventTypeName, new List<Type>());
            }
            if (_integrationEventHandlers[eventTypeName].Contains(typeof(TIntegrationEventHandler)))
            {
                throw new ArgumentException($"Handler {typeof(TIntegrationEventHandler).Name} already registered for '{eventTypeName}'");
            }

            _integrationEventHandlers[eventTypeName].Add(typeof(TIntegrationEventHandler));

            // If event type is a new type of event add to list.
            if (!_eventTypes.Contains(typeof(TIntegrationEvent)))
            {
                _eventTypes.Add(typeof(TIntegrationEvent));
            }
        }

        public Type GetEventTypeByName(string eventName)
        {
            return _eventTypes.SingleOrDefault(x => x.Name == eventName);
        }

        public bool HasSubscriptionsForEvent(string eventName)
        {
            var exists = _integrationEventHandlers.ContainsKey(eventName);
            return exists;
        }

        public string GetEventKey<TIntegrationEvent>()
        {
            return typeof(TIntegrationEvent).Name;
        }

        public List<Type> GetIntegrationEventHandlersForEvent(string eventName)
        {
            return _integrationEventHandlers[eventName];
        }

        #endregion
    }
}
