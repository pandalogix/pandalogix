using System.Collections.Generic;
using System.Linq;
using Engine.Contracts;

namespace PadManager.Core.Models
{
    public class Pad : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Node> Nodes { get; set; }
        public List<InstanceMapping> InstanceMappings { get; set; }

        //keep track of node sequence
        public int CurrentMaxSequenceId { get; set; }

        //the trigger data load schema
        public string TriggerData { get; set; }
    }

    public static class PadExtension
    {
        public static PadContract ToContract(this Pad pad){
            var contract = new PadContract(){
                Id = pad.Id,
                Name = pad.Name,
                Description = pad.Description,
                TriggerData = pad.TriggerData,
                Nodes = pad.Nodes.Select(n=>n.ToContract())
            };
            return contract;
        }
    }
}