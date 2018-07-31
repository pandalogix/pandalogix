using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace EventBus
{
    public class InMemoryEventBus : IEventBus
    {
        private readonly IServiceProvider serviceProvider;
        private readonly IEventBusSubscriptionsManager evtSubManger;
        private BlockingCollection<Event> cq;

        public InMemoryEventBus(IEventBusSubscriptionsManager manager, IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            evtSubManger = manager;
            evtSubManger.OnEventRemoved += OnEventRemoved;
            cq = new BlockingCollection<Event>();
            Task.Factory.StartNew(() =>
            {
                foreach (var item in cq.GetConsumingEnumerable())
                {
                    DispatchEvent(item);
                }
            });
        }

        private void DispatchEvent(object item)
        {
            var evnetName = item.GetType().Name;
            if (evtSubManger.HasSubscriptionsForEvent(evnetName))
            {
                using (var scopt = this.serviceProvider.CreateScope())
                {
                    var subscriptions = evtSubManger.GetHandlersForEvent(evnetName);
                    foreach (var sub in subscriptions)
                    {
                        var eventType = evtSubManger.GetEventTypeByName(evnetName);
                        var handler = scopt.ServiceProvider.GetService(sub.HandlerType);
                        var concreteType = typeof(IEventHandler<>).MakeGenericType(eventType);
                        concreteType.GetMethod("Handle").Invoke(handler, new object[] { item });

                    }
                }
            }
        }

        private void OnEventRemoved(object sender, string e)
        {
            Console.WriteLine($"{e} is removed");
        }

        public Task Publish(Event @event)
        {
            cq.Add(@event);
            cq.CompleteAdding();
            return Task.CompletedTask;
        }

        public void Subscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            var evnetName = evtSubManger.GetEventKey<T>();
            evtSubManger.AddSubscription<T, TH>();
        }

        public void UnSubscribe<T, TH>()
            where T : Event
            where TH : IEventHandler<T>
        {
            evtSubManger.RemoveSubscription<T, TH>();
        }
    }
}