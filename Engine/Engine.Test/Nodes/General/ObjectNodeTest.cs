using Engine.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Engine.Core;
using Engine.Core.Nodes.General;
using System;
using System.Collections.Generic;
using Xunit;
using System.Threading.Tasks;
using Engine.Enums;
using Engine.Contracts;

namespace Engine.CoreTest.Nodes.General
{
  public class ObjectNodeTest
  {
    [Fact]
    public async Task ObjectNodeTestAsync()
    {
      var padContract = new Engine.Contracts.PadContract()
      {
        Id = 1,
        Name = "test"
      };

      var objectNode = new Engine.Contracts.NodeBaseContract()
      {
        Id = 1,
        NodeId = 1,
        OutNodes = new List<long>() { 2 },
        Type = NodeType.Input,
        MetaData = new NodeMetaData()
        {
          NodeData = new NodeMetaDataAttribute() { NodeClass = typeof(ObjectNode) },
          FieldsMetaData = new List<FieldMetaDataAttribute>()
          {
            new FieldMetaDataAttribute()
            {
              Name = "Schema",
              ValueType = typeof(string)
            },
            new FieldMetaDataAttribute()
            {
              Name = "JsonString",
              ValueType = typeof(string)
            }
          }
        }
      };

      var constant1 = new Engine.Contracts.NodeBaseContract()
      {
        Id = 2,
        NodeId = 2,
        // OutNodes = new List<long>() { 3 },
        Type = NodeType.Output,
        MetaData = new NodeMetaData()
        {
          NodeData = new NodeMetaDataAttribute() { NodeClass = typeof(ConstantNode) },
          FieldsMetaData = new List<FieldMetaDataAttribute>()
              {
                new FieldMetaDataAttribute()
                {
                   Name = "Value",
                   ValueType = typeof(object),
                   Direction =  FieldDirection.Input,
                   MappedNodeId =1,
                   MappedFieldName ="__Result__"
                },
                new FieldMetaDataAttribute()
                {
                  Name ="ConstantType",
                  ValueType = typeof(ConstantType)
                }
              }
        }
      };

      padContract.Nodes = new List<Engine.Contracts.NodeBaseContract>()
      {
        objectNode,constant1
      };

      var contractString = JsonConvert.SerializeObject(padContract);

      JSchemaGenerator generator = new JSchemaGenerator();
      JSchema schema = generator.Generate(typeof(Account));
      var schemaString = schema.ToString();
      var account = new Account
      {
        Email = "1@test.com",
        Name = "Johe Doe",
        Age = 20,
        DateBrith = new DateTimeOffset(1980, 1, 1, 12, 1, 2, new TimeSpan(-6, 0, 0)),
        Address = new Address { Line1 = "Mobile AL" },
        Duration = new TimeSpan(12, 0, 1),
        LastLogin = DateTime.Now
      };

      List<InstanceMapping> mappings = new List<InstanceMapping>()
      {
        new InstanceMapping()
        {
           NodeId=1,
           FieldMappings = new List<FieldMapping>()
           {
             new FieldMapping(){ FieldName = "Schema", Value=schema.ToString() },
              new FieldMapping(){ FieldName = "JsonString", Value=JsonConvert.SerializeObject(account) },
           }
        }
      };
      var instance = new Instances(mappings);
      var json = JsonConvert.SerializeObject(instance);
      var pad = PadFactory.CreateInstance(padContract, ExecutionMode.Normal, instance);
      await pad.Init();
      await pad.Execute(pad.Context, instance);

      Assert.Equal(ExecutionStatus.Success, pad.Context.Status);
      Assert.NotNull(pad.Context.Result);

      await Task.CompletedTask;
    }

    [Fact]
    public async System.Threading.Tasks.Task ObjNodeTestAsync()
    {
      JSchemaGenerator generator = new JSchemaGenerator();
      JSchema schema = generator.Generate(typeof(Account));
      var node = new ObjectNode();
      node.InPorts = new List<INode>();
      node.LogicPath = true;
      node.Schema = schema.ToString();
      var account = new Account
      {
        Email = "1@test.com",
        Name = "Johe Doe",
        Age = 20,
        DateBrith = new DateTimeOffset(1980, 1, 1, 12, 1, 2, new TimeSpan(-6, 0, 0)),
        Address = new Address { Line1 = "Mobile AL" },
        Duration = new TimeSpan(12, 0, 1),
        LastLogin = DateTime.Now
      };
      node.JsonString = JsonConvert.SerializeObject(account);
      await node.Init(new PadExecutionContext() { Pad = new Pad(Engine.Enums.ExecutionMode.Normal) { Nodes = new List<INode>() } });
      await node.Execute(node.Context);
      Assert.NotNull(node.ObjectValue);

    }

    public class Account
    {
      public string Email;
      public string Name { get; set; }
      public Address Address { get; set; }
      public int Age { get; set; }
      public DateTimeOffset DateBrith { get; set; }
      public DateTime LastLogin { get; set; }
      public TimeSpan Duration { get; set; }
    }

    public class Address
    {
      public string Line1 { get; set; }
    }
  }
}
