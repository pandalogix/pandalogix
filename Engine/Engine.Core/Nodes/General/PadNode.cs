using Engine;
using Engine.Contracts;
using Engine.Enums;
using Engine.Interfaces;
using System.Threading.Tasks;

namespace PandaDoctor.Nodes.General
{
    [NodeMetaData(Category = "General", NodeClass = typeof(PadNode), Name = nameof(PadNode))]
    public class PadNode : NodeBase
    {
        [FieldMetaData(Name = "PadContract", ValueType = typeof(PadContract))]
        public PadContract PadContract { get; set; }
        [FieldMetaData(Name = nameof(PadIdentifier), ValueType = typeof(string))]
        public string PadIdentifier { get; set; }

        [FieldMetaData(Name = "PadMode",ValueType =typeof(ExecutionMode))]
        public ExecutionMode PadMode { get; set; }

        [FieldMetaData(Name = nameof(Instance), ValueType = typeof(Instances))]
        public Instances Instance { get; set; }


        protected override async Task InternalExcute(IContext context)
        {
            var pad = PadFactory.CreateInstance(this.PadContract, this.PadMode, this.Instance);
            await pad.Init();
            await pad.Execute(pad.Context, Instance);
            this.Context.Result = pad.Context.Result;
            this.Context.ExecutionSummary = pad.Context.ExecutionSummary;
            this.Context.Status = pad.Context.Status;
        }
    }
}
