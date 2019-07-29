using System.Collections.Generic;
using Engine.Core;
using Engine.Enums;
using Engine.Interfaces;
using Xunit;

namespace Engine.Contracts
{
  public class StringFormatNodeTest
  {
    [Fact]
    public async System.Threading.Tasks.Task StringFormatTest()
    {
      var node = new StringFormatNode();
      node.Pattern = @"Hello {{name}}, from {{name}} {{value}}";
      node.Data = @"{'name':'James', 'value':'ddddd'}";


      var context = new NodeExecutionContext()
      {
        Pad = new Pad(ExecutionMode.Validation)
        { Nodes = new List<INode>() }
      };
      await node.Init(context);
      await node.Execute(context);
      Assert.DoesNotContain("{", node.Context.Result.ToString());
    }


    [Fact]
    public async System.Threading.Tasks.Task StringFormatFromNodesTest()
    {
      var node = new StringFormatNode();
      node.Pattern = @"Hello {{name_1}}, from {{name_3}} {{value_2}}";
      node.FromOtherNodes = true;

      node.MetaDate = new NodeMetaData()
      {
        NodeData = new NodeMetaDataAttribute() { NodeClass = typeof(StringFormatNode) },
        FieldsMetaData = new List<FieldMetaDataAttribute>()
               {
                 new FieldMetaDataAttribute()
                 {
                     MappedNodeId =1,
                     MappedFieldName ="name"
                 },
                new FieldMetaDataAttribute()
                 {
                     MappedNodeId =2,
                     MappedFieldName ="value"
                 },
                 new FieldMetaDataAttribute()
                 {
                     MappedNodeId =3,
                     MappedFieldName ="name"
                 },
               }
      };

      var context = new NodeExecutionContext()
      {
        Pad = new Pad(ExecutionMode.Validation)
        { Nodes = new List<INode>() }
      };
      await node.Init(context);
      await node.Execute(context);
      Assert.DoesNotContain("{", node.Context.Result.ToString());
    }
  }
}