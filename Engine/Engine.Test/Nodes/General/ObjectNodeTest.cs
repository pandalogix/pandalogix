using Engine.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Engine.Core;
using Engine.Core.Nodes.General;
using System;
using System.Collections.Generic;
using Xunit;

namespace Engine.CoreTest.Nodes.General
{
    public class ObjectNodeTest
    {
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
