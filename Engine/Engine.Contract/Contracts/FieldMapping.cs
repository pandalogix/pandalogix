using System.Runtime.Serialization;

namespace Engine.Contracts
{
  [DataContract(Name = "FieldMapping")]
  public class FieldMapping
  {
    [DataMember]
    public string FieldName { get; set; }
    [DataMember]
    public string Value { get; set; }
  }
}
