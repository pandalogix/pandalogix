using System;
namespace EventBus
{
  public class Event
  {
    public Event()
    {
      Id = Guid.NewGuid();
      CreateDate = DateTime.UtcNow;
    }
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
  }

}