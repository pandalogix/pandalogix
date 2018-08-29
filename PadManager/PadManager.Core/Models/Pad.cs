using System.Collections.Generic;

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
}