using System.Collections.Generic;
using System.Linq;
using Engine.Contracts;

namespace PadManager.Core.Models
{
  public class Pad : BaseEntity
  {
    public string Name { get; set; }
    public string Description { get; set; }
    public List<Node> Nodes { get; set; }
    public List<InstanceMapping> InstanceMappings { get; set; }

    //keep track of node sequence
    public int CurrentMaxSequenceId { get; set; }

    //the trigger data load schema
    public string TriggerData { get; set; }
  }

  public static class PadExtension
  {
    public static PadContract ToContract(this Pad pad)
    {
      var contract = new PadContract()
      {
        Id = pad.Id,
        Identifier = pad.Identifier,
        Name = pad.Name,
        Description = pad.Description,
        TriggerData = pad.TriggerData,
      };
      var nodes = new List<NodeBaseContract>();
      if (pad.Nodes != null)
      {
        foreach (var node in pad.Nodes)
        {
          nodes.Add(node.ToContract());
        }
      }
      contract.Nodes = nodes;
      return contract;
    }

    public static Pad ToModel(this PadContract pad)
    {
      var model = new Pad();
      model.Id = pad.Id;
      model.Description = pad.Description;
      model.Name = pad.Name;
      model.Identifier = pad.Identifier;
      model.TriggerData = pad.TriggerData;
      model.Nodes = pad.Nodes?.Select(n => n.ToModel()).ToList();
      model.CurrentMaxSequenceId = pad.CurrentMaxSequenceId;
      return model;
    }
  }
}