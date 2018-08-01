using Engine;
using Engine.Helpers;
using Engine.Interfaces;
using System;
using System.Threading.Tasks;

namespace Engine.Core.Nodes.Math
{
    [NodeMetaData(NodeClass = typeof(SubstractNode), Category = "Math", Name = nameof(SubstractNode))]
  public class SubstractNode : DualInputBaseNode
  {
    protected override async Task InternalExecute(IContext context)
    {
      var left = GetFieldValue(nameof(Left));
      var right = GetFieldValue(nameof(Right));
      var leftType = left.GetType();
      var rightType = right.GetType();
      if (leftType.IsNumericType() && rightType.IsNumericType())
      {
        this._context.Result = Convert.ToDouble(left) - Convert.ToDouble(right);
      }
      else if (leftType.IsDateTimeType() && rightType.IsDateTimeType())
      {
        if (leftType == typeof(DateTime) && rightType == typeof(DateTime))
        {
          throw new InvalidOperationException("Do not support Datetime addition");
        }
        if (leftType == typeof(DateTime) && rightType == typeof(TimeSpan))
        {
          _context.Result = Convert.ToDateTime(left).Subtract((TimeSpan)right);
        }
        if (rightType == typeof(DateTime) && leftType == typeof(TimeSpan))
        {
          _context.Result = Convert.ToDateTime(right).Subtract((TimeSpan)left);
        }
      }
      else
      {
        throw new InvalidOperationException($"Type are not match");
      }
      await base.InternalExecute(context);
    }
  }
}
