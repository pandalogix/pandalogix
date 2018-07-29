using Engine.Enums;
using Engine.Interfaces;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Engine.Contracts
{
  [DataContract]
  public class NodeBaseContract : IIdentifiableEntity
  {
    [DataMember]
    public NodeType Type { get; set; }
    [DataMember]
    public IEnumerable<long> InNodes { get; set; } = new List<long>();
    [DataMember]
    public IEnumerable<long> OutNodes { get; set; } = new List<long>();
    [DataMember]
    public NodeMetaData MetaData { get; set; }

    [DataMember]
    public bool LogicPath { get; set; } = true;
    [DataMember]
    public long Id { get; set; }
  }
}
