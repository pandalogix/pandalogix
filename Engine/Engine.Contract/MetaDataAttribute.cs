using Engine.Enums;
using System;
using System.Runtime.Serialization;

namespace Engine
{
  [DataContract]
  [AttributeUsage(AttributeTargets.Property)]
  public class FieldMetaDataAttribute : Attribute
  {
    private string _valueTypeString;
    private Type _valueType;
    [DataMember]
    public string Name { get; set; }
    [DataMember]
    public string DefaultValue { get; set; }
    [DataMember]
    public string ConstantValue { get; set; }
    [DataMember]
    public string ValueTypeString
    {
      get => _valueTypeString;
      set
      {
        if (!string.IsNullOrEmpty(value))
        {
          _valueTypeString = value;
          _valueType = Type.GetType(value);
        }
      }
    }
    public Type ValueType
    {
      get => _valueType; set
      {
        if (value != null)
        {
          _valueType = value;
          _valueTypeString = value.AssemblyQualifiedName;
        }
      }
    }
    [DataMember]
    public FieldDirection Direction { get; set; } = FieldDirection.Input;
    [DataMember]
    public long MappedNodeId { get; set; } = -1;
    [DataMember]
    public string MappedFieldName { get; set; } = Constants.DEFAULT_MAPFIELDNAME;

  }


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
  }
}
