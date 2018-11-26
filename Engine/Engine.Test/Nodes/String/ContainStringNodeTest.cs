using Engine;
using Engine.Contracts;
using Engine.Enums;
using Newtonsoft.Json;
using Engine.Core;
using Engine.Core.Nodes;
using Engine.Core.Nodes.General;
using Engine.Core.Nodes.Math;
using System.Collections.Generic;
using Xunit;
using System;

namespace Engine.CoreTest
{
  public class ContainStringNodeTest
  {
    [Fact]
    public async void ContainStringNode()
    {
      var padContract = new Engine.Contracts.PadContract()
      {
        Id = 1,
        Name = "test"
      };

      var constant1 = new Engine.Contracts.NodeBaseContract()
      {
        Id = 1,
        NodeId = 1,
        OutNodes = new List<long>() { 3 },
        Type = NodeType.Input,
        MetaData = new NodeMetaData()
        {
          NodeData = new NodeMetaDataAttribute() { NodeClass = typeof(ConstantNode) },
          FieldsMetaData = new List<FieldMetaDataAttribute>()
              {
                new FieldMetaDataAttribute()
                {
                   Name = "Value",
                   ValueType = typeof(string)
                },
                new FieldMetaDataAttribute()
                {
                  Name ="ConstantType",
                  ValueType = typeof(ConstantType)
                }
              }
        }
      };

      var constant2 = new Engine.Contracts.NodeBaseContract()
      {
        Id = 2,
        NodeId = 2,
        OutNodes = new List<long>() { 3 },
        Type = NodeType.Input,
        MetaData = new NodeMetaData()
        {
          NodeData = new Engine.NodeMetaDataAttribute() { NodeClass = typeof(ConstantNode) },
          FieldsMetaData = new List<FieldMetaDataAttribute>()
              {
                new FieldMetaDataAttribute()
                {
                   Name = "Value",
                    ValueType = typeof(string)
                },
                new FieldMetaDataAttribute()
                {
                  Name ="ConstantType",
                  ValueType = typeof(ConstantType)
                }
              }
        }
      };

      var add = new Engine.Contracts.NodeBaseContract()
      {
        Id = 3,
        NodeId = 3,
        InNodes = new List<long>() { 1, 2 },
        Type = NodeType.Output,
        MetaData = new NodeMetaData()
        {
          NodeData = new Engine.NodeMetaDataAttribute() { NodeClass = typeof(ContainStringNode) },
          FieldsMetaData = new List<FieldMetaDataAttribute>()
          {
            new FieldMetaDataAttribute()
            {
              Name="SubString",
              Direction =  FieldDirection.Input,
              MappedNodeId =1,
              MappedFieldName ="Value"
            },
            new FieldMetaDataAttribute()
            {
              Name="Value",
               MappedNodeId =2,
              Direction =  FieldDirection.Input,
            }
          }
        }
      };

      padContract.Nodes = new List<Engine.Contracts.NodeBaseContract>()
      {
        constant1,constant2,add
      };

      List<InstanceMapping> mappings = new List<InstanceMapping>()
      {
        new InstanceMapping()
        {
           NodeId=1,
           FieldMappings = new List<FieldMapping>()
           {
             new FieldMapping(){ FieldName = "Value", Value="hello" },
              new FieldMapping(){ FieldName = "ConstantType", Value="String" },
           }
        },
         new InstanceMapping()
        {
           NodeId=2,
           FieldMappings = new List<FieldMapping>()
           {
             new FieldMapping(){ FieldName = "Value", Value="hello world" },
              new FieldMapping(){ FieldName = "ConstantType", Value="String" },
           }
        }
      };
      var instance = new Instances(mappings);

      var json = JsonConvert.SerializeObject(instance);

      var pad = PadFactory.CreateInstance(padContract, ExecutionMode.Normal, instance);
      await pad.Init();
      await pad.Execute(pad.Context, instance);

      Assert.Equal(ExecutionStatus.Success, pad.Context.Status);
      Assert.Equal(true, pad.Context.Result);
    }


  }
}