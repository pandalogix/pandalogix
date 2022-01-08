using System.Text.Json.Serialization;
using Engine.Enums;
using Engine.Interfaces;

namespace Engine.Core
{
  public class NodeExecutionContext : IContext
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

  }
}
