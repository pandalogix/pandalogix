using System;

namespace WebSPA.Controllers
{
  public class Pad
  {
    public long Id { get; set; }
    public Guid Identifier { get; set; }
    Guid UserId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string TriggerData { get; set; }

  }
}