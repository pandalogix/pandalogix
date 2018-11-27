using System;
using Engine.Contracts;

namespace WebSPA.Controllers
{
  internal class PadExecutionEvent
  {
    public Guid UserId { get; set; }
    public PadContract Pad { get; set; }
    public Instances Instances { get; set; }
  }

}