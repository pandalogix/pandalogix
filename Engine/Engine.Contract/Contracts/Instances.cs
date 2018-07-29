using Engine.Interfaces;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Engine.Contracts
{
  [DataContract(Name = "Instances")]
  [KnownType(typeof(InstanceMapping))]
  public class Instances : IInstance
  {
    IEnumerable<InstanceMapping> _mappings;
    public Instances(IEnumerable<InstanceMapping> mappings)
    {
      Mappings = mappings;
    }
    [DataMember]
    public IEnumerable<InstanceMapping> Mappings { get => _mappings; set => _mappings = value; }

    public IEnumerable<InstanceMapping> GetMappings()
    {
      return Mappings;
    }
  }
}
