using Engine;
using Engine.Enums;
using Engine.Interfaces;
using System;
using System.Threading.Tasks;

namespace PandaDoctor.Nodes.General
{
    [NodeMetaData(NodeClass =typeof(ConstantNode), Category ="General",Name =nameof(ConstantNode))]
  public class ConstantNode : NodeBase
  {
    [FieldMetaData(Name =nameof(Value),ValueType =typeof(object))]
    public object Value { get; set; }
    [FieldMetaData(Name = nameof(ConstantType), ValueType = typeof(ConstantType))]
    public  ConstantType ConstantType { get; set; }

    public ConstantNode()
    {
      this.Type = NodeType.Input;
    }


    protected async override Task InternalExcute(IContext context)
    {

      if (this.Type == NodeType.Input)
      {
        switch (this.ConstantType)
        {
          case ConstantType.String:
            context.Result = this.Value;
            break;
          case ConstantType.Int:
            context.Result = Convert.ToInt32(this.Value);
            break;
          case ConstantType.Number:
            context.Result = Convert.ToDouble(this.Value);
            break;
          case ConstantType.Decimal:
            context.Result = Convert.ToDecimal(this.Value);
            break;
          case ConstantType.Date:
            context.Result = Convert.ToDateTime(this.Value);
            break;
          case ConstantType.Time:
            context.Result = new TimeSpan();
            break;
          case ConstantType.DateTime:
            context.Result = Convert.ToDateTime(this.Value);
            break;
          case ConstantType.Boolean:
            context.Result = Convert.ToBoolean(this.Value);
            break;
        }
      }else if(this.Type == NodeType.Output)
      {
        context.Result = this.GetFieldValue(nameof(Value));
      }
      this.Value = context.Result;
      await base.InternalExcute(context);
    }
  }
}
