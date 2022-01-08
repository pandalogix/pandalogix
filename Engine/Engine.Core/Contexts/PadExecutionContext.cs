

using Engine.Enums;
using Engine.Interfaces;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Engine.Core
{
  public class PadExecutionContext : IContext
  {
    [JsonIgnore]
    public IPad Pad { get; set; }
    [JsonIgnore]
    public IInstance Instance { get; set; }
    [JsonIgnore]
    public ExecutionMode Mode { get; set; }
    public string ExecutionSummary { get; set; }
    public object Result { get; set; }
    public ExecutionStatus Status { get; set; } = ExecutionStatus.Pending;

    public Dictionary<long, NodeExecutionContext> NodeExecutionContext { get; set; } =
      new Dictionary<long, NodeExecutionContext>();

  }
}
