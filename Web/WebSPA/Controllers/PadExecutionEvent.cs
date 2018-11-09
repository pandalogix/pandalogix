using Engine.Contracts;

namespace WebSPA.Controllers
{
  internal class PadExecutionEvent
  {
        public PadContract Pad { get; set; }
        public Instances Instances { get; set; }
    }
  
}