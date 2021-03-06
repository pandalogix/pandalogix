﻿

using Engine.Enums;
using Engine.Interfaces;

namespace Engine.Core
{
  public class PadExecutionContext : IContext
  {
    public IPad Pad { get; set; }
    public IInstance Instance { get; set; }
    public ExecutionMode Mode { get; set; }
    public string ExecutionSummary { get; set; }
    public object Result { get; set; }
    public ExecutionStatus Status { get; set; } = ExecutionStatus.Pending;

  }
}
