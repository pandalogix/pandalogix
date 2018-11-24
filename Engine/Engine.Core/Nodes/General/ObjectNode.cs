using System;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Engine.Exceptions;
using Engine.Interfaces;
using Engine;
using Engine.Enums;

namespace Engine.Core.Nodes.General
{
  [NodeMetaData(NodeClass = typeof(ObjectNode), Category = "General", Name = nameof(ObjectNode))]

  public class ObjectNode : NodeBase
  {
    [FieldMetaData(Name = nameof(JsonString), ValueType = typeof(string))]
    public string JsonString { get; set; }
    [FieldMetaData(Name = nameof(Schema), ValueType = typeof(string))]
    public string Schema { get; set; }
    [FieldMetaData(Name = nameof(ObjectValue), ValueType = typeof(Object), Direction = FieldDirection.Output)]
    public Object ObjectValue { get; set; }

    protected override async Task InternalExecute(IContext context)
    {
      if (string.IsNullOrEmpty(Schema))
        throw new ValidationException("Schema can't be empty");
      var schema = JSchema.Parse(Schema);
      JObject jObj = JObject.Parse(JsonString);
      if (jObj.IsValid(schema))
      {
        ObjectValue = jObj;
        context.Result = jObj;
      }
      await base.InternalExecute(context);
    }
  }
}
