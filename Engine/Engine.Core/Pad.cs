using Engine.Enums;
using Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Engine.Core
{
  public class Pad : IPad
  {
    private IContext _context;
    private ExecutionMode _mode;

    public Pad(ExecutionMode mode)
    {
      _mode = mode;
    }


    public IContext Context
    {
      get { return _context; }
      set { _context = value; }
    }

    public IEnumerable<INode> Nodes { get; set; }

    public async Task Execute(IContext context, IInstance instance)
    {
      try
      {
        this.Context=context;
        this.Context.Instance = instance;
        var inputs = (from o in Nodes
                      where o.Type == NodeType.Input
                      select o).ToList<INode>();
        if (inputs.Count > 0)
        {
          this._context.Status = ExecutionStatus.Executing;
          foreach (var node in inputs)
          {
            await this.NodeExecute(node);

          }
        }
        this._context.Status = ExecutionStatus.Success;
      }

      catch (Exception e)
      {
        this._context.Status = ExecutionStatus.Failed;
        this._context.ExecutionSummary = $"Failed with error:{e}";
      }
      finally{
        //saving history
      }
    }

    private async Task NodeExecute(INode node)
    {
      if (node.Context == null)
        await node.Init(this.Context);

      if (node.Context.Status == ExecutionStatus.Pending)
        await node.Execute(node.Context);

      foreach (var outNode in node.OutPorts)
      {
        if (!(from o in outNode.InPorts where (o.Context == null || o.Context.Status != ExecutionStatus.Success) select o).Any())
          await this.NodeExecute(outNode);
      }
    }


    public Task<IContext> Init(IContext context=null)
    {
        var c = context as PadExecutionContext;

      _context = new PadExecutionContext
      {
        Pad = this,
        Mode = _mode,
        Instance = null,
        ExecutionSummary = c?.ExecutionSummary?? string.Empty,
        Status = c?.Status??ExecutionStatus.Pending,
        Result =c?.Result,
        NodeExecutionContext =c?.NodeExecutionContext??new Dictionary<long, NodeExecutionContext>()
      };

      return Task.FromResult<IContext>(_context);
    }
  }
}
