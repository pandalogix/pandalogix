using System;
using System.Collections.Generic;
using System.Linq;

namespace EventBus
{
    public class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        private readonly Dictionary<string, List<SubscriptionInfo>> handlers;

        public readonly List<Type> evnetTypes;

        public InMemoryEventBusSubscriptionsManager()
        {
            handlers = new Dictionary<string, List<SubscriptionInfo>>();
            evnetTypes = new List<Type>();
        }

        public bool IsEmpty => !handlers.Keys.Any();


        public event EventHandler<string> OnEventRemoved;

        public void AddSubscription<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var eventName = this.GetEventKey<T>();
            if (!HasSubscriptionsForEvent(eventName))
            {
                handlers.Add(eventName, new List<SubscriptionInfo>());
            }
            if (handlers[eventName].Any(s => s.HandlerType == typeof(T)))
            {

                throw new ArgumentException(
                       $"Handler Type {typeof(TH).Name} already registered for '{eventName}'");
            }
            handlers[eventName].Add(new SubscriptionInfo(typeof(TH)));
            evnetTypes.Add(typeof(T));
        }

        public void Clear() => handlers.Clear();

        public string GetEventKey<T>() => typeof(T).Name;

        public Type GetEventTypeByName(string eventName) => evnetTypes.SingleOrDefault(t => t.Name == eventName);

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : Event => this.GetHandlersForEvent(typeof(T).Name);

        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => handlers[eventName];

        public bool HasSubscriptionsForEvent<T>() where T : Event => this.HasSubscriptionsForEvent(typeof(T).Name);

        public bool HasSubscriptionsForEvent(string eventName) => handlers.ContainsKey(eventName);

        public void RemoveSubscription<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var eventName = this.GetEventKey<T>();
            if (HasSubscriptionsForEvent(eventName))
            {
                var subscription = handlers[eventName].SingleOrDefault(s => s.HandlerType == typeof(TH));
                if (subscription != null)
                {
                    handlers[eventName].Remove(subscription);
                    if (!handlers[eventName].Any())
                    {
                        handlers.Remove(eventName);
                        var evnetType = evnetTypes.SingleOrDefault(s => s.Name == eventName);
                        if (evnetType != null)
                        {
                            evnetTypes.Remove(evnetType);
                        }
                        if (OnEventRemoved != null)
                        {
                            OnEventRemoved(this, eventName);
                        }
                    }
                }
            }
        }
    }
}