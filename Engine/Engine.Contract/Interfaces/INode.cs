using Engine.Enums;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Engine.Interfaces
{
  public interface INode
  {
    Task<IContext> Init(IContext parent);
    Task Execute(IContext context);
    IContext Context { get; }
    IEnumerable<INode> InPorts { get; set; }
    IEnumerable<INode> OutPorts { get; set; }

    NodeType Type { get; set; }
  }
}
