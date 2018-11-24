using Engine;
using Engine.Helpers;
using Engine.Interfaces;
using System;
using System.Threading.Tasks;

namespace Engine.Core.Nodes.Math
{
  [NodeMetaData(NodeClass = typeof(DivideNode), Category = "Math", Name = nameof(DivideNode))]
  public class DivideNode : DualInputBaseNode
  {
    protected override async Task InternalExecute(IContext context)
    {
      var left = GetFieldValue(nameof(Left));
      var right = GetFieldValue(nameof(Right));
      var leftType = left.GetType();
      var rightType = right.GetType();
      if (leftType.IsNumericType() && rightType.IsNumericType())
      {
        this._context.Result = Convert.ToDouble(left) / Convert.ToDouble(right);
      }
      else
      {
        throw new InvalidOperationException("Only numeric value supported");
      }
      await base.InternalExecute(context);
    }
  }
}
