using System;
using System.Threading.Tasks;
using Engine.Core.Nodes;
using Engine.Interfaces;

namespace Engine.Core
{
  [NodeMetaData(NodeClass = typeof(RepeatNode), Category = "Collection", Name = "Repeat", NodeIcon="fal fa-repeat-1-alt")]

  public class RepeatNode : NodeBase
  {
    [FieldMetaData(Name = nameof(Count), ValueType = typeof(int))]
    public int Count { get; set; }

    protected async override Task InternalExecute(IContext context)
    {
      int length;
      int.TryParse(GetFieldValue(nameof(Count)).ToString(), out length);


      for (int i = 0; i < length; i++)
      {
        //call padnode..
      }


      await base.InternalExecute(context);
    }
  }
}