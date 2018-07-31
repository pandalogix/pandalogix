using Engine;
using Engine.Interfaces;
using System;
using System.Threading.Tasks;

namespace PandaDoctor.Nodes.Logic
{
    [NodeMetaData(NodeClass = typeof(GreaterThanOrEqualNode), Category = "Logic", Name = nameof(GreaterThanOrEqualNode))]

  public class GreaterThanOrEqualNode : DualInputBaseNode
  {

    protected override async Task InternalExecute(IContext context)
    {
      if (Left is IComparable && Right is IComparable && Left.GetType() == Right.GetType())
      {
        var left = Left as IComparable;
        var right = Right as IComparable;
        this.Context.Result = left.CompareTo(right) >= 0;
      }
      else
      {
        throw new Exception("Unmatch objects");
      }
      await base.InternalExecute(context);
    }
  }
}
