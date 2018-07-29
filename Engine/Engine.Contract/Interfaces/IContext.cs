using Engine.Enums;

namespace Engine.Interfaces
{
  public interface IContext
  {
    IPad Pad { get; set; }
    IInstance Instance { get; set; }

    ExecutionMode Mode { get; set; }

    string ExecutionSummary { get; set; }

    object Result { get; set; }
    ExecutionStatus Status { get; set; }
  }
}