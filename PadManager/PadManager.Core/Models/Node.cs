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
        if (string.IsNullOrEmpty(InNodeList))
          return null;
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
        if (string.IsNullOrEmpty(OutNodesList))
          return null;
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

    public string NodeIdentifier { get; set; }
  }

  public static class NodeExtension
  {
    public static NodeBaseContract ToContract(this Node node)
    {
      try
      {
        var contract = new NodeBaseContract();
        contract.Type = (NodeType)Enum.Parse(typeof(NodeType), node.NodeType);
        contract.Id = node.Id;
        contract.InNodes = node.InNodes == null ? new List<long>() : node.InNodes;
        contract.OutNodes = node.OutNodes == null ? new List<long>() : node.OutNodes;
        contract.NodeId = node.NodeId;
        contract.MetaData = Newtonsoft.Json.JsonConvert.DeserializeObject<NodeMetaData>(node.MetaData);
        return contract;
      }
      catch (Exception)
      {
        return null;
      }
    }

    public static Node ToModel(this NodeBaseContract node)
    {
      var model = new Node();
      model.Id = node.Id;
      // model.NodeId = node.NodeId;
      model.InNodes = node.InNodes == null ? new List<long>() : node.InNodes.ToList();
      model.OutNodes = node.OutNodes == null ? new List<long>() : node.OutNodes.ToList();
      model.MetaData = JsonConvert.SerializeObject(node.MetaData);
      model.Location = node.Location;
      model.NodeType = node.Type.ToString();
      model.NodeId = node.NodeId;
      return model;
    }
  }
}