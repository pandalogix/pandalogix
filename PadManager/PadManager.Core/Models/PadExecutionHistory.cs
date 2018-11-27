using System;
using Engine.Enums;

namespace PadManager.Core.Models
{
  public class PadExecutionHistory : BaseEntity
  {
    public Guid PadIdentifier { get; set; }
    public string ExecutionSummary { get; set; }
    public string Result { get; set; }
    public ExecutionStatus Status { get; set; } = ExecutionStatus.Pending;

  }
}