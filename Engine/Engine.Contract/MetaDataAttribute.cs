using System;
using System.Runtime.Serialization;

namespace Engine
{


  [DataContract]
  [AttributeUsage(AttributeTargets.Class)]
  public class NodeMetaDataAttribute : Attribute
  {
    private string _nodeClassString;
    private Type _nodeClass;
    [DataMember]
    public string NodeClassString
    {
      get
      {
        return this._nodeClassString;
      }
      set
      {
        if (!string.IsNullOrEmpty(value))
        {
          _nodeClassString = value;
          _nodeClass = Type.GetType(value);
        }
      }
    }

    public Type NodeClass
    {
      get
      {
        return this._nodeClass;
      }
      set
      {
        if (value != null)
        {
          this._nodeClass = value;
          this._nodeClassString = this._nodeClass.AssemblyQualifiedName;
        }
      }
    }
    [DataMember]
    public string Category { get; set; }
    [DataMember]
    public string Name { get; set; }

    [DataMember]
    public string NodeIcon { get; set; }
  }
}
