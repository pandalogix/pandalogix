using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Engine.Contracts
{
  [DataContract]
  public class NodeMetaData
  {
    [DataMember]
    public NodeMetaDataAttribute NodeData { get; set; }

    [DataMember]
    public List<FieldMetaDataAttribute> FieldsMetaData { get; set; }
  }
}