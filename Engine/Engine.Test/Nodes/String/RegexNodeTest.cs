using System.Collections.Generic;
using Engine.Core;
using Engine.Enums;
using Engine.Interfaces;
using Xunit;

namespace Engine.Test
{
  public class RegexNodeTest
  {
    [Fact]
    public async System.Threading.Tasks.Task RegexNodeMatchTestAsync()
    {
      var node = new RegexNode();
      node.Pattern = @"(\d{3})-(\d{3}-\d{4})";
      node.Value = "212-555-6666 906-932-1111 415-222-3333 425-888-9999";
      node.EnableGroup = true;
      var context = new NodeExecutionContext()
      {
        Pad = new Pad(ExecutionMode.Validation)
        { Nodes = new List<INode>() }
      };
      await node.Init(context);
      await node.Execute(context);
      Assert.Equal(true, node.Context.Result);
      Assert.Equal(4, node.Matches.Count);
    }


  }
}