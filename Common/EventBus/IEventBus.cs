using System;
using System.Threading.Tasks;

namespace EventBus
{
  public interface IEventBus
  {
    Task Publish(Event @event);

    void Subscribe<T, TH>()
      where T : Event
      where TH : IEventHandler<T>;

    void UnSubscribe<T, TH>()
      where T : Event
      where TH : IEventHandler<T>;

  }
}
