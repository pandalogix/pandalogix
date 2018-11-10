using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Engine.Contracts;
using Engine.Enums;
using Newtonsoft.Json;

namespace PadManager.Core.Models
{
  public class Node : BaseEntity
  {
    public Pad Pad { get; set; }

    public int NodeId { get; set; }

    [NotMapped]
    public List<long> InNodes
    {
      get
      {
        return System.Array.ConvertAll(InNodeList.Split(';'), long.Parse).ToList();
      }
      set
      {
        InNodeList = String.Join(";", value.Select(p => p.ToString()).ToArray());
      }
    }
    [NotMapped]
    public List<long> OutNodes
    {
      get
      {
        return System.Array.ConvertAll(OutNodesList.Split(';'), long.Parse).ToList();
      }
      set
      {
        OutNodesList = String.Join(";", value.Select(p => p.ToString()).ToArray());
      }
    }
    [JsonIgnore]
    [Column("InNodes")]
    public string InNodeList { get; set; }
    [JsonIgnore]
    [Column("OutNodes")]
    public string OutNodesList { get; set; }
    public string MetaData { get; set; }

    public string NodeType { get; set; }

    public string Location { get; set; }
  }

  public static class NodeExtension
  {
    public static NodeBaseContract ToContract(this Node node)
    {
      var contract = new NodeBaseContract();
      contract.Type = (NodeType)Enum.Parse(typeof(NodeType), node.NodeType);
      contract.Id = node.Id;
      contract.InNodes = node.InNodes;
      contract.OutNodes = node.OutNodes;
      contract.MetaData = Newtonsoft.Json.JsonConvert.DeserializeObject<NodeMetaData>(node.MetaData);
      return contract;
    }

    public static Node ToModel(this NodeBaseContract node)
    {
      var model = new Node();
      model.Id = node.Id;
      // model.NodeId = node.NodeId;
      model.InNodes = node.InNodes.ToList();
      model.OutNodes = node.OutNodes.ToList();
      model.MetaData = JsonConvert.SerializeObject(node.MetaData);
      model.Location = node.Location;
      model.NodeType = node.Type.ToString();
      return model;
    }
  }
}