using System.Threading.Tasks;
using DynamicExpresso;
using Engine.Core.Nodes;
using Engine.Interfaces;
using HandlebarsDotNet;
using Newtonsoft.Json.Linq;

namespace Engine.Core
{
    [NodeMetaData(NodeClass = typeof(ExpressionEvaluatorNode), Category = "General", Name = nameof(ExpressionEvaluatorNode))]

    public class ExpressionEvaluatorNode : NodeBase
    {
        [FieldMetaData(Name = nameof(Expression), ValueType = typeof(string))]
        public string Expression { get; set; } = null;
        protected async override Task InternalExecute(IContext context)
        {
            var d = new JObject();
            foreach (var f in this.MetaDate.FieldsMetaData)
            {
                if (f.MappedNodeId != this.Id || f.MappedNodeId != 0)
                {
                    d.Add($"{f.MappedFieldName}_{f.MappedNodeId}", new JValue(GetFieldValue(f.MappedFieldName, f.MappedNodeId)));
                }
            }
            var pattern = this.Expression ?? GetFieldValue(nameof(Expression)).ToString();
            var template = Handlebars.Compile(pattern);
            var realexpression = template(d);
            var interpreter = new Interpreter();
            this.Context.Result = interpreter.Eval(realexpression);
            await base.InternalExecute(context);
        }
    }
}