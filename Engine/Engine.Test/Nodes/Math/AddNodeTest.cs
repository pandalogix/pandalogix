using Engine;
using Engine.Contracts;
using Engine.Enums;
using Newtonsoft.Json;
using PandaDoctor;
using PandaDoctor.Nodes;
using PandaDoctor.Nodes.General;
using PandaDoctor.Nodes.Math;
using System.Collections.Generic;
using Xunit;
using System;

namespace PandaDoctorTest
{
    public class AddNodeTest
    {
        [Fact]
        public async void AddNodeConstantNumberTest()
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

            var add = new Engine.Contracts.NodeBaseContract()
            {
                Id = 3,
                InNodes = new List<long>() { 1, 2 },
                Type = NodeType.Output,
                MetaData = new NodeMetaData()
                {
                    NodeData = new Engine.NodeMetaDataAttribute() { NodeClass = typeof(AddNode) },
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

            var json = JsonConvert.SerializeObject(instance);

            var pad = PadFactory.CreateInstance(padContract, ExecutionMode.Normal, instance);
            await pad.Init();
            await pad.Execute(pad.Context, instance);

            Assert.Equal(ExecutionStatus.Success, pad.Context.Status);
            Assert.Equal(2.0, pad.Context.Result);
        }

        [Fact]
        public async void AddNodeConstantDateTimeTest()
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
                   ValueType = typeof(DateTime)
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
                    ValueType = typeof(TimeSpan)
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
                    NodeData = new Engine.NodeMetaDataAttribute() { NodeClass = typeof(AddNode) },
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
      var now = DateTime.Now;
            List<InstanceMapping> mappings = new List<InstanceMapping>()
      {
        new InstanceMapping()
        {
           NodeId=1,
           FieldMappings = new List<FieldMapping>()
           {
             new FieldMapping(){ FieldName = "Value", Value=now.ToString() },
              new FieldMapping(){ FieldName = "ConstantType", Value="DateTime" },
           }
        },
         new InstanceMapping()
        {
           NodeId=2,
           FieldMappings = new List<FieldMapping>()
           {
             new FieldMapping(){ FieldName = "Value", Value="1:00:00" },
              new FieldMapping(){ FieldName = "ConstantType", Value="TimeSpan" },
           }
        }
      };
            var instance = new Instances(mappings);

            var json = JsonConvert.SerializeObject(instance);

            var pad = PadFactory.CreateInstance(padContract, ExecutionMode.Normal, instance);
            await pad.Init();
            await pad.Execute(pad.Context, instance);

            Assert.Equal(ExecutionStatus.Success, pad.Context.Status);
            Assert.Equal(now.AddHours(1).ToString(), pad.Context.Result.ToString());
        }


        [Fact]
        public async void AddNodeConstantStringTest()
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
                InNodes = new List<long>() { 1, 2 },
                Type = NodeType.Output,
                MetaData = new NodeMetaData()
                {
                    NodeData = new Engine.NodeMetaDataAttribute() { NodeClass = typeof(AddNode) },
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
             new FieldMapping(){ FieldName = "Value", Value="hello " },
              new FieldMapping(){ FieldName = "ConstantType", Value="String" },
           }
        },
         new InstanceMapping()
        {
           NodeId=2,
           FieldMappings = new List<FieldMapping>()
           {
             new FieldMapping(){ FieldName = "Value", Value="World" },
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
            Assert.Equal("hello World", pad.Context.Result);
        }
    }


}
