using System.Collections.Generic;
using System.Dynamic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Engine.Core.Nodes;
using Engine.Interfaces;
using HandlebarsDotNet;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Engine.Core
{
  [NodeMetaData(NodeClass = typeof(StringFormatNode), Category = "String", Name="Format", NodeIcon="fal fa-text")]

  public class StringFormatNode : NodeBase
  {
    [FieldMetaData(Name = nameof(Pattern), ValueType = typeof(string))]
    public string Pattern { get; set; } = null;

    [FieldMetaData(Name = nameof(Data), ValueType = typeof(string))]
    public string Data { get; set; } = null;

    [FieldMetaData(Name = nameof(FromOtherNodes), ValueType = typeof(bool))]
    public bool FromOtherNodes { get; set; } = false;

    protected async override Task InternalExecute(IContext context)
    {
      object data = null;
      string pattern = Pattern ?? GetFieldValue(nameof(Pattern)).ToString();
      if (this.FromOtherNodes)
      {
        var d = new JObject();
        foreach (var f in this.MetaDate.FieldsMetaData)
        {
          if (f.MappedNodeId != this.Id || f.MappedNodeId != 0)
          {
            d.Add($"{f.MappedFieldName}_{f.MappedNodeId}", new JValue(GetFieldValue(f.MappedFieldName)));
          }
        }
        data = d;
      }
      else
      {
        var dd = Data ?? GetFieldValue(nameof(Data)).ToString();
        data = JsonConvert.DeserializeObject(dd);
      }
      var template = Handlebars.Compile(pattern);
      this.Context.Result = template(data);
      await base.InternalExecute(context);
    }
  }
}