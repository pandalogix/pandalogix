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
    private PadExecutionContext _context;
    private ExecutionMode _mode;

    public Pad(ExecutionMode mode)
    {
      _mode = mode;
    }


    public IContext Context => _context;

    public IEnumerable<INode> Nodes { get; set; }

    public async Task Execute(IContext context, IInstance instance)
    {
      try
      {
        await this.Init();
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


    public Task<IContext> Init()
    {
      _context = new PadExecutionContext
      {
        Pad = this,
        Mode = _mode,
        Instance = null,
        ExecutionSummary = string.Empty,
        Status = ExecutionStatus.Pending
      };
      return Task.FromResult<IContext>(_context);
    }
  }
}
