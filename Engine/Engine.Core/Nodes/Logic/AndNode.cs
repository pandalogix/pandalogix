using Engine;
using Engine.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Engine.Core.Nodes.Logic
{
  [NodeMetaData(NodeClass = typeof(AndNode), Category = "Logic", Name = "And", NodeIcon="")]
  public class AndNode : NodeBase
  {
    protected override async Task InternalExecute(IContext context)
    {

      var result = true;

      this._context.Result = this.InPorts.Aggregate(result, (d, node) =>
       {
         if (node.Context != null)
         {
           return d && (node.Context.Result is bool ? Convert.ToBoolean(node.Context.Result) : node.Context.Result != null);
         }
         else
         {
           return d && false;
         }
       });

      await base.InternalExecute(context);
    }
  }
}
