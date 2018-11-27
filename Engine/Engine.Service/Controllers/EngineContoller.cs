using System;
using System.Net.Http;
using System.Threading.Tasks;
using Engine.Contract.Contracts;
using Engine.Contracts;
using Engine.Core;
using Engine.Enums;
using EventBus;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Engine.Service
{
  [Route("api/[controller]")]
  //[ApiController]
  public class EngineController : Controller
  {
    private readonly IMediator mediator;
    private readonly IEventBus eventBus;
    private readonly IHttpClientFactory clientFactory;

    public EngineController(IMediator mediator, IEventBus evtBus, IHttpClientFactory clientFactory)
    {
      this.mediator = mediator;
      this.eventBus = evtBus;
      this.clientFactory = clientFactory;
    }

    [HttpPost]
    [Route("event")]
    public async Task<IActionResult> Execute([FromBody]PadExecution pad)
    {
      await this.eventBus.Publish(pad);
      return Ok();
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> ExecuteSync([FromBody] PadExecution request)
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
      return Ok(result);

    }
  }

  public class PadExecution : Event
  {
    public Guid UserId { get; set; }
    public PadContract Pad { get; set; }
    public Instances Instances { get; set; }
  }

  public class PadExecutionHandler : IEventHandler<PadExecution>
  {
    private readonly IMediator mediator;

    public PadExecutionHandler(IMediator mediator)
    {
      this.mediator = mediator;
    }
    public async Task Handle(PadExecution pad)
    {
      var result = await this.mediator.Send(new ExecutionCommand(pad.Pad, pad.Instances,pad.UserId));
    }
  }
}