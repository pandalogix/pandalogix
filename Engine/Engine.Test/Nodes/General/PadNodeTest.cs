using Engine;
using Engine.Contracts;
using Engine.Enums;
using Newtonsoft.Json;
using PandaDoctor;
using PandaDoctor.Nodes.General;
using PandaDoctor.Nodes.Math;
using System.Collections.Generic;
using Xunit;


namespace PandaDoctorTest.Nodes.General
{
    public class PadNodeTest
    {
        [Fact]
        public async System.Threading.Tasks.Task PadNodeExeTestAsync()
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
                    NodeData = new NodeMetaDataAttribute() { NodeClass = typeof(ConstantNode) },
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

            var constant2 = new NodeBaseContract()
            {
                Id = 2,
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

            var add = new NodeBaseContract()
            {
                Id = 3,
                InNodes = new List<long>() { 1, 2 },
                Type = NodeType.Output,
                MetaData = new NodeMetaData()
                {
                    NodeData = new NodeMetaDataAttribute() { NodeClass = typeof(AddNode) },
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

            padContract.Nodes = new List<NodeBaseContract>()
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
             new FieldMapping(){ FieldName = "Value", Value="1" },
              new FieldMapping(){ FieldName = "ConstantType", Value="Int" },
           }
        },
         new InstanceMapping()
        {
           NodeId=2,
           FieldMappings = new List<FieldMapping>()
           {
             new FieldMapping(){ FieldName = "Value", Value="1" },
              new FieldMapping(){ FieldName = "ConstantType", Value="Int" },
           }
        }
      };
            var instance = new Instances(mappings);


            var padnodeContract = new Engine.Contracts.PadContract()
            {
                Id = 1,
                Name = "test"
            };

            var padNode = new NodeBaseContract()
            {
                Id = 1,
                Type = NodeType.Input,
                OutNodes = new List<long>() { 2 },
                MetaData = new NodeMetaData()
                {
                    NodeData = new NodeMetaDataAttribute() { NodeClass = typeof(PadNode) },
                    FieldsMetaData = new List<FieldMetaDataAttribute>()
              {
                new FieldMetaDataAttribute()
                {
                   Name = "PadContract",
                   ValueType = typeof(Engine.Contracts.PadContract)
                },
                new FieldMetaDataAttribute()
                {
                  Name ="PadMode",
                  ValueType = typeof(ExecutionMode)
                },
                new FieldMetaDataAttribute()
                {
                  Name ="Instance",
                  ValueType = typeof(Instances)
                }
              }
                }
            };
            var constantOutput = new NodeBaseContract()
            {
                Id = 2,
                Type = NodeType.Output,
                MetaData = new NodeMetaData()
                {
                    NodeData = new NodeMetaDataAttribute() { NodeClass = typeof(ConstantNode) },
                    FieldsMetaData = new List<FieldMetaDataAttribute>()
              {
                new FieldMetaDataAttribute()
                {
                   Name = "Value",
                     MappedNodeId =1,
              Direction =  FieldDirection.Input,
                },
                new FieldMetaDataAttribute()
                {
                  Name ="ConstantType",
                  ValueType = typeof(ConstantType)
                }
              }
                }
            };
            padnodeContract.Nodes = new List<NodeBaseContract>()
      {
        padNode,constantOutput
      };

            var padInstance = new Instances(
              new List<InstanceMapping>()
              {
          new InstanceMapping()
          {
             NodeId=1,
             FieldMappings = new List<FieldMapping>()
             {
               new FieldMapping(){ FieldName = "PadContract", Value=JsonConvert.SerializeObject(padContract) },
                new FieldMapping(){ FieldName = "PadMode", Value="Normal" },
                new FieldMapping(){ FieldName = "Instance", Value=JsonConvert.SerializeObject(instance)},
             }
          }

              });



            var pad = PadFactory.CreateInstance(padnodeContract, ExecutionMode.Normal, padInstance);
            await pad.Init();
            await pad.Execute(pad.Context, instance);

            Assert.Equal(ExecutionStatus.Success, pad.Context.Status);
            Assert.Equal(2.0, pad.Context.Result);
        }
    }
}
