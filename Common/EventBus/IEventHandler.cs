using System.Threading.Tasks;
namespace EventBus
{
    public interface IEventHandler<in TEvent>
    where TEvent:Event
    {
        Task Handle(TEvent eventData);
    }
    
}