using Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Engine.Contracts
{
  [DataContract]
  public class PadContract : IIdentifiableEntity
  {
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public string Description { get; set; }
    [DataMember]
    public IEnumerable<NodeBaseContract> Nodes { get; set; }

    [DataMember]
    public long Id { get; set; }

    [DataMember]
    public string TriggerData { get; set; }


  }
}
