using Engine;
using Engine.Interfaces;
using System;
using System.Threading.Tasks;

namespace PandaDoctor.Nodes.Logic
{
    [NodeMetaData(NodeClass = typeof(LessThanNode), Category = "Logic", Name = nameof(LessThanNode))]

  public class LessThanNode : DualInputBaseNode
  {
    protected override async Task InternalExecute(IContext context)
    {
      var left = GetFieldValue(nameof(Left));
      var right = GetFieldValue(nameof(Right));
      if(left is IComparable && right is IComparable && left.GetType() == right.GetType())
      {
        var leftc = Left as IComparable;
        var rightc = Right as IComparable;
        this.Context.Result = leftc.CompareTo(rightc)< 0;
      }
      else
      {
        throw new Exception("Unmatch objects");
      }
     await  base.InternalExecute(context);
    }
  }
}
