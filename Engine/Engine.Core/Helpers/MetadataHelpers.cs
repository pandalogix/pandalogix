using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Engine.Contracts;

namespace Engine.Core.Helpers
{
  public static class MetadataHelpers
  {
    public static Dictionary<string, List<NodeMetaData>> GetMetadata()
    {
      Assembly engine = typeof(Pad).Assembly;
      var nodestypes = engine.GetTypes().Where(t => t.GetCustomAttribute(typeof(NodeMetaDataAttribute)) != null).Select(t => t).ToList();
      Dictionary<string, List<NodeMetaData>> data = new Dictionary<string, List<NodeMetaData>>();
      foreach (var t in nodestypes)
      {
        var meta = new NodeMetaData()
        {
          NodeData = t.GetCustomAttribute<NodeMetaDataAttribute>(),
          FieldsMetaData = t.GetProperties().Where(p => p.GetCustomAttribute(typeof(FieldMetaDataAttribute)) != null).Select(p => p.GetCustomAttribute<FieldMetaDataAttribute>())
        };
        if (data.ContainsKey(meta.NodeData.Category))
        {
          data[meta.NodeData.Category].Add(meta);
        }
        else
        {
          data.Add(meta.NodeData.Category, new List<NodeMetaData>() { meta });
        }
      }
      return data;
    }


  }
}