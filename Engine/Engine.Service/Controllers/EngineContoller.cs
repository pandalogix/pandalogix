using System.Threading.Tasks;
using Engine.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Engine.Service
{
    [Route("api/[controller]")]
    //[ApiController]
    public class EngineController : Controller
    {
        private readonly IMediator mediator;

        public EngineController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Execute([FromBody]PadExecution pad)
        {
            var result = await this.mediator.Send(new ExecutionCommand(pad.Pad,pad.Instances));
            if(result)
            return Ok();
            else
            return StatusCode(500);
        }
    }

    public class PadExecution
    {
        public PadContract Pad { get; set; }
        public Instances Instances { get; set; }
    }
}