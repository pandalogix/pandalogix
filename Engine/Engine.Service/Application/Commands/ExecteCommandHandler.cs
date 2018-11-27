using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Engine.Contract.Contracts;
using Engine.Core;
using Engine.Enums;
using MediatR;

namespace Engine.Service
{

  public class ExecuteCommandHandler : IRequestHandler<ExecutionCommand, bool>
  {
    private IHttpClientFactory clientFactory;

    public ExecuteCommandHandler(IHttpClientFactory clientFacotory)
    {
      this.clientFactory = clientFacotory;
    }
    public async Task<bool> Handle(ExecutionCommand request, CancellationToken cancellationToken)
    {
      var pad = PadFactory.CreateInstance(request.Pad, ExecutionMode.Normal, request.Instances);
      await pad.Init();
      await pad.Execute(pad.Context, request.Instances);

      var result = new ExecutionResult
      {
        PadIdentifier = request.Pad.Identifier,
        Status = pad.Context.Status,
        Summary = pad.Context.ExecutionSummary,
        Result = pad.Context.Result
      };

      using (var padmgr = this.clientFactory.CreateClient("padMgr"))
      {
        var histResponse = await padmgr.PostAsJsonAsync<ExecutionResult>($"/api/pad/history?userId={request.UserId}", result);
      }
      return true;
    }
  }
}