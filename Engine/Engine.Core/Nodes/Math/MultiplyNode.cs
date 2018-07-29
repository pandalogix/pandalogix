﻿using Engine;
using Engine.Helpers;
using Engine.Interfaces;
using System;
using System.Threading.Tasks;

namespace PandaDoctor.Nodes.Math
{
    [NodeMetaData(NodeClass = typeof(MultiplyNode), Category = "Math", Name = nameof(MultiplyNode))]
  public class MultiplyNode : DualInputBaseNode
  {
    protected override async Task InternalExcute(IContext context)
    {
      var left = GetFieldValue(nameof(Left));
      var right = GetFieldValue(nameof(Right));
      var leftType = left.GetType();
      var rightType = right.GetType();
      if (leftType.IsNumericType() && rightType.IsNumericType())
      {
        this._context.Result = Convert.ToDouble(left) * Convert.ToDouble(right);
      }
      else
      {
        throw new InvalidOperationException("Only numeric value supported");
      }
      await base.InternalExcute(context);
    }
  }
}
