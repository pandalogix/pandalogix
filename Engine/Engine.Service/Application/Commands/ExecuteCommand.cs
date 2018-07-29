using System.Runtime.Serialization;
using Engine.Contracts;
using MediatR;

namespace Engine.Service
{
    public class ExecutionCommand : IRequest<bool>
    {
        [DataMember]
        public Instances Instances { get; set; }
        [DataMember]
        public PadContract Pad { get; set; }
        public ExecutionCommand(PadContract contract, Instances instances)
        {
            this.Pad = contract;
            this.Instances = instances;
        }
    }
}