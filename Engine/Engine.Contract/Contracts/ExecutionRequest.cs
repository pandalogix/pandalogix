using System;
namespace Engine.Contracts;

public class ExecutionRequest
  {
    public Guid UserId { get; set; }
    public PadContract Pad { get; set; }
    public Instances Instances { get; set; }
  }