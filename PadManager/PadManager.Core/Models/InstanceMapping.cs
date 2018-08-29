namespace PadManager.Core.Models
{
    public class InstanceMapping : BaseEntity
    {
        public Pad Pad { get; set; }

        public string FieldMappings { get; set; }
    }
}