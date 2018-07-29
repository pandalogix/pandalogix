
using Engine.Enums;

namespace Engine.Contracts
{
  public class Field
  {
    public string Name { get; set; }
    public FieldValueCategory Categroy { get; set; }

    public ConstantType ValueType { get; set; }

    public object Value { get; set; }
  }
}
