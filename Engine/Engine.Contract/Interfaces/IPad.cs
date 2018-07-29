using System.Collections.Generic;
using System.Threading.Tasks;

namespace Engine.Interfaces
{
  public interface IPad
  {
    Task<IContext> Init();
    Task Execute(IContext context, IInstance instance);

    IContext Context { get; }

    IEnumerable<INode> Nodes { get; set; }
  }
}
