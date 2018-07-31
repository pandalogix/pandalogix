
using Engine.Enums;
using Engine.Interfaces;
using PandaDoctor;
using PandaDoctor.Nodes.General;
using System.Collections.Generic;
using System.Diagnostics;
using Xunit;

namespace PandaDoctorTest.Nodes.General
{

    public class RestNodeTest
    {
        [Fact]
        public async System.Threading.Tasks.Task RestGetTestAsync()
        {
            var node = new RestNode();
            node.EndPoint = "http://httpbin.org/get";
            node.Method = "get";

            await node.Init(new PadExecutionContext() { Pad = new PandaDoctor.Pad(ExecutionMode.Normal) { Nodes = new List<INode>() } });
            await node.Execute(node.Context);
            Assert.Equal(ExecutionStatus.Success, node.Context.Status);
        }

        [Fact]
        public async System.Threading.Tasks.Task RestPostTestAsync()
        {
            var node = new RestNode();
            node.EndPoint = "http://httpbin.org/post";
            node.Method = "post";
            node.Body = "Hello World";
            await node.Init(new PadExecutionContext() { Pad = new PandaDoctor.Pad(ExecutionMode.Normal) { Nodes = new List<INode>() } });
            await node.Execute(node.Context);
            Debug.WriteLine(node.Context.Result);
            Assert.Equal(ExecutionStatus.Success, node.Context.Status);

        }
    }
}
