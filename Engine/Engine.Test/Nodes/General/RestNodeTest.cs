
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
    public void RestGetTest()
    {
      var node = new RestNode();
      node.EndPoint = "http://httpbin.org/get";
      node.Method = "get";
     
      node.Init(new PadExecutionContext() { Pad = new PandaDoctor.Pad(ExecutionMode.Normal) { Nodes = new List<INode>() } });
      node.Execute(node.Context);

    }

    [Fact]
    public void RestPostTest()
    {
      var node = new RestNode();
      node.EndPoint = "http://httpbin.org/post";
      node.Method = "post";
      node.Body = "Hello World";
      node.Init(new PadExecutionContext() { Pad = new PandaDoctor.Pad(ExecutionMode.Normal) { Nodes = new List<INode>() } });
      node.Execute(node.Context);
      Debug.WriteLine(node.Context.Result);
    }
  }
}
