using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Engine.Contracts
{
  [DataContract(Name = "InstanceMapping")]
  [KnownType(typeof(FieldMapping))]
  public class InstanceMapping
    {
    [DataMember]
    public long  NodeId { get; set; }
    [DataMember]
    public IEnumerable<FieldMapping> FieldMappings { get; set; }
  }
}
