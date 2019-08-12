using Engine;
using Engine.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Engine.Core.Nodes.Logic
{
  [NodeMetaData(NodeClass = typeof(OrNode), Category = "Logic", Name="Or", NodeIcon="")]
  public class OrNode : NodeBase
  {

    protected override async Task InternalExecute(IContext context)
    {
      var result = false;

      this._context.Result = this.InPorts.Aggregate(result, (d, node) =>
      {
        if (node.Context != null)
        {
          return d || (node.Context.Result is bool ? Convert.ToBoolean(node.Context.Result) : node.Context.Result != null);
        }
        else
        {
          return d || false;
        }
      });

      await base.InternalExecute(context);
    }
  }
}
