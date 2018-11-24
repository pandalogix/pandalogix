using System.Threading.Tasks;
using Engine.Contracts;
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

    public EngineController(IMediator mediator, IEventBus evtBus)
    {
      this.mediator = mediator;
      this.eventBus = evtBus;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Execute([FromBody]PadExecution pad)
    {
      await this.eventBus.Publish(pad);
      return Ok();
    }
  }

  public class PadExecution : Event
  {
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
      var result = await this.mediator.Send(new ExecutionCommand(pad.Pad, pad.Instances));
    }
  }
}