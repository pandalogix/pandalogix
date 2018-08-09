using Engine;
using Engine.Interfaces;
using System;
using System.Threading.Tasks;

namespace Engine.Core.Nodes.Logic
{
    [NodeMetaData(NodeClass = typeof(EqualNode), Category = "Logic", Name = nameof(EqualNode))]
    public class EqualNode : DualInputBaseNode
    {
        protected override async Task InternalExecute(IContext context)
        {
            var left = GetFieldValue(nameof(Left));
            var right = GetFieldValue(nameof(Right));
            if (left is IComparable && right is IComparable && left.GetType() == right.GetType())
            {
                var leftc = left as IComparable;
                var rightc = right as IComparable;
                this.Context.Result = leftc.CompareTo(rightc) == 0;
            }
            else
            {
                throw new Exception("Unmatch objects");
            }
            await base.InternalExecute(context);
        }
    }
}
