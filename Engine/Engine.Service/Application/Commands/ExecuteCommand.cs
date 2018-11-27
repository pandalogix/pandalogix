using System;
using System.Runtime.Serialization;
using Engine.Contracts;
using MediatR;

namespace Engine.Service
{
  public class ExecutionCommand : IRequest<bool>
  {
    [DataMember]
    public Instances Instances { get; set; }
    [DataMember]
    public PadContract Pad { get; set; }
    [DataMember]
    public Guid UserId { get; set; }

    public ExecutionCommand(PadContract contract, Instances instances,Guid userId)
    {
      this.UserId = userId;
      this.Pad = contract;
      this.Instances = instances;
    }
  }
}