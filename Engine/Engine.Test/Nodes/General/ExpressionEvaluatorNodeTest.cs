using System.Collections.Generic;
using System.Threading.Tasks;
using Engine.Contracts;
using Engine.Core;
using Engine.Core.Nodes.General;
using Engine.Enums;
using Newtonsoft.Json;
using Xunit;

namespace Engine.Test
{
    public class ExpressionEvaluatorNodeTest
    {
        [Fact]
        public async Task ExpressionEvNode()
        {
            var padContract = new Engine.Contracts.PadContract()
            {
                Id = 1,
                Name = "test"
            };

            var constant1 = new Engine.Contracts.NodeBaseContract()
            {
                Id = 1,
                OutNodes = new List<long>() { 4 },
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
                OutNodes = new List<long>() { 4 },
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


            var constant3 = new Engine.Contracts.NodeBaseContract()
            {
                Id = 3,
                OutNodes = new List<long>() { 4 },
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

            var expressionNode = new Engine.Contracts.NodeBaseContract()
            {
                Id = 4,
                InNodes = new List<long>() { 1, 2, 3 },
                Type = NodeType.Output,
                MetaData = new NodeMetaData()
                {
                    NodeData = new NodeMetaDataAttribute() { NodeClass = typeof(ExpressionEvaluatorNode) },
                    FieldsMetaData = new List<FieldMetaDataAttribute>()
                    {
                      new FieldMetaDataAttribute()
                      {
                        Name="Value",
                        MappedNodeId=1,
                        MappedFieldName="Value"
                      },
                      new FieldMetaDataAttribute()
                      {
                        Name="Value",
                        MappedNodeId=2,
                        MappedFieldName="Value"
                      },
                      new FieldMetaDataAttribute()
                      {
                        Name="Value",
                        MappedNodeId=3,
                        MappedFieldName="Value"
                      },
                      new FieldMetaDataAttribute()
                      {
                        Name ="Expression",
                        ValueType = typeof(string)
                      }
                    }
                }
            };

            padContract.Nodes = new List<Engine.Contracts.NodeBaseContract>()
            {
              constant1,constant2,constant3,expressionNode
            };

            List<InstanceMapping> mappings = new List<InstanceMapping>()
            {
              new InstanceMapping()
              {
                NodeId=1,
                FieldMappings = new List<FieldMapping>()
                {
                  new FieldMapping(){ FieldName = "Value", Value="2" },
                  new FieldMapping(){ FieldName = "ConstantType", Value="Int" },
                }
              },
              new InstanceMapping()
              {
                NodeId=2,
                FieldMappings = new List<FieldMapping>()
                {
                  new FieldMapping(){ FieldName = "Value", Value="10" },
                  new FieldMapping(){ FieldName = "ConstantType", Value="Int" },
                }
              },
              new InstanceMapping()
              {
                NodeId=3,
                FieldMappings = new List<FieldMapping>()
                {
                  new FieldMapping(){ FieldName = "Value", Value="1" },
                  new FieldMapping(){ FieldName = "ConstantType", Value="Int" },
                }
              },
              new InstanceMapping()
              {
                NodeId=4,
                FieldMappings = new List<FieldMapping>()
                {
                  new FieldMapping(){ FieldName = "Expression", Value="{{Value_1}} * {{Value_2}} + {{Value_3}}" },
                }
              }
            };
            var instance = new Instances(mappings);

            var json = JsonConvert.SerializeObject(instance);

            var pad = PadFactory.CreateInstance(padContract, ExecutionMode.Normal, instance);
            await pad.Init();
            await pad.Execute(pad.Context, instance);

            Assert.Equal(ExecutionStatus.Success, pad.Context.Status);
            Assert.Equal(21, pad.Context.Result);
        }
    }
}