using System.Data;
using System.Threading.Tasks;
using Engine.Core.Nodes;
using Engine.Interfaces;

namespace Engine.Core
{
    [NodeMetaData(NodeClass = typeof(CollectionFilterNode), Category = "Collection", Name = nameof(CollectionFilterNode))]
    public class CollectionFilterNode : NodeBase
    {
        [FieldMetaData(Name = nameof(Data), ValueType = typeof(DataTable))]
        public DataTable Data { get; set; }

        [FieldMetaData(Name = nameof(FilterExpression), ValueType = typeof(string))]
        public string FilterExpression { get; set; }
        protected async override Task InternalExecute(IContext context)
        {
            var data = Data ?? GetFieldValue(nameof(Data)) as DataTable;
            var filter = FilterExpression ?? GetFieldValue(nameof(FilterExpression)) as string;
            this.Context.Result = data.Select(filter);
            await base.InternalExecute(context);
        }
    }
}