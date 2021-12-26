using Microsoft.AspNetCore.Mvc;
using Engine.Contracts;
using Engine.Enums;
using Engine.Core;
namespace Engine.Executor.Controllers;

[ApiController]
[Route("[controller]")]
public class ExcutorController : ControllerBase
{
  private ILogger<ExcutorController> _logger;
  public ExcutorController(ILogger<ExcutorController> logger) => _logger = logger;

  [HttpPost()]
  public async Task<ExecutionResult> Execute([FromBody] ExecutionRequest request)
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
    return result;
  }
}

