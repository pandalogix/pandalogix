using Engine;

namespace Engine.Core.Nodes
{
  public abstract class DualInputBaseNode : NodeBase
  {
    [FieldMetaData(Name = nameof(Left))]
    public object Left { get; set; }
    [FieldMetaData(Name = nameof(Right))]
    public object Right { get; set; }
  }
}
