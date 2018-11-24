using Engine.Contracts;
using Engine.Core.Nodes;
using Engine.Enums;
using Engine.Interfaces;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.Core
{
  public static class PadFactory
  {
    public static Pad CreateInstance(PadContract contract, ExecutionMode mode, IInstance instance)
    {

      Pad pad = new Pad(mode);

      var nodes = new List<Nodes.NodeBase>();
      foreach (var nodecontract in contract.Nodes)
      {
        nodes.Add(NodeFactory.CreateNode(nodecontract, instance.GetMappings()));
      }
      pad.Nodes = nodes;

      // fill the in and out nodes

      foreach (var nodecontract in contract.Nodes)
      {
        Nodes.NodeBase node = (from o in nodes where o.Id == nodecontract.Id select o).FirstOrDefault();
        if (node != null)
        {
          if (nodecontract.InNodes != null)
          {
            node.InPorts = (from o in nodes where nodecontract.InNodes.Contains(o.Id) select o)?.ToList<NodeBase>();
          }
          if (nodecontract.OutNodes != null)
          {
            node.OutPorts = (from o in nodes where nodecontract.OutNodes.Contains(o.Id) select o)?.ToList<NodeBase>();
          }
        }
      }

      return pad;
    }


  }
}
