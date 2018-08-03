using System.Collections.Generic;
using Engine.Core;
using Engine.Enums;
using Engine.Interfaces;
using Xunit;

namespace Engine.Contracts
{
    public class StringFormatNodeTest
    {
        [Fact]
        public async System.Threading.Tasks.Task StringFormatTest()
        {
            var node = new StringFormatNode();
            node.Pattern = @"Hello {{name}}, from {{name}} {{value}}";
            node.Data = @"{'name':'James', 'value':'ddddd'}";


            var context = new NodeExecutionContext()
            {
                Pad = new Pad(ExecutionMode.Validation)
                { Nodes = new List<INode>() }
            };
            await node.Init(context);
            await node.Execute(context);
            Assert.False(node.Context.Result.ToString().Contains("{"));
        }
    }
}