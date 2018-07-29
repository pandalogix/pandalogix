using Engine;
using Engine.Contracts;
using Engine.Enums;
using PandaDoctor;
using PandaDoctor.Nodes.General;
using PandaDoctor.Nodes.Math;
using System.Collections.Generic;
using Xunit;

namespace PandaDoctorTest
{
    public class DivideTest
  {
    [Fact]
    public void DivideTestNodeConstantTest()
    {
      var padContract = new Engine.Contracts.PadContract()
      {
        Id = 1,
        Name = "test"
      };

      var constant1 = new Engine.Contracts.NodeBaseContract()
      {
        Id = 1,
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
                   ValueType = typeof(int)
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
                    ValueType = typeof(double)
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
        InNodes = new List<long>() { 1, 2 },
        Type = NodeType.Output,
        MetaData = new NodeMetaData()
        {
          NodeData = new Engine.NodeMetaDataAttribute() { NodeClass = typeof(DivideNode) },
          FieldsMetaData = new List<FieldMetaDataAttribute>()
          {
            new FieldMetaDataAttribute()
            {
              Name="Left",
              Direction =  FieldDirection.Input,
              MappedNodeId =1,
              MappedFieldName ="Value"
            },
            new FieldMetaDataAttribute()
            {
              Name="Right",
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
             new FieldMapping(){ FieldName = "Value", Value="10" },
              new FieldMapping(){ FieldName = "ConstantType", Value="Int" },
           }
        },
         new InstanceMapping()
        {
           NodeId=2,
           FieldMappings = new List<FieldMapping>()
           {
             new FieldMapping(){ FieldName = "Value", Value="1.1" },
              new FieldMapping(){ FieldName = "ConstantType", Value="Number" },
           }
        }
      };
      var instance = new Instances(mappings);


      var pad = PadFactory.CreateInstance(padContract, ExecutionMode.Normal, instance);
      pad.Init();
      pad.Execute(pad.Context, instance);

      Assert.Equal(ExecutionStatus.Success, pad.Context.Status);
      Assert.Equal(10/1.1, pad.Context.Result);
    }
  }
}
