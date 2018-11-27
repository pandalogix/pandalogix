using System;
using Engine.Enums;

namespace Engine.Contract.Contracts
{
  public class ExecutionResult
  {
    public Guid PadIdentifier { get; set; }
    public ExecutionStatus Status { get; set; }
    public string Summary { get; set; }
    public object Result { get; set; }

  }
}