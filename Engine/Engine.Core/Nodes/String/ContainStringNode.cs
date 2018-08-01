using System.Threading.Tasks;
using Engine.Core.Nodes;
using Engine.Interfaces;

namespace Engine.Contracts
{
    [NodeMetaData(NodeClass = typeof(ContainStringNode), Category = "String", Name = nameof(ContainStringNode))]

    public class ContainStringNode : NodeBase
    {
        [FieldMetaData(Name = nameof(SubString), ValueType = typeof(string))]
        public string SubString { get; set; } = null;

        [FieldMetaData(Name = nameof(Value), ValueType = typeof(string))]
        public string Value { get; set; } = null;

        protected async override Task InternalExecute(IContext context)
        {
            string substring = SubString ?? GetFieldValue(nameof(SubString)).ToString();
            string value = Value ?? GetFieldValue(nameof(Value)).ToString();
            this.Context.Result = value.Contains(substring);
            await base.InternalExecute(context);
        }
    }

}