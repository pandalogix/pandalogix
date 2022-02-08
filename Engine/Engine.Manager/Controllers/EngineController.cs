namespace Engine.Manager.Controllers;

[ApiController]
[Route("[controller]")]
public class EngineController : ControllerBase
{
  private ILogger<EngineController> _logger;
  private readonly IHttpClientFactory _httpClientFactory;

  public EngineController(ILogger<EngineController> logger, IHttpClientFactory httpClientFactory)
  {
    _logger = logger;
    _httpClientFactory = httpClientFactory;
  }

  [HttpPost("execute")]
  public async Task ExecutePad(Guid padid, [FromBody] Instances instance)
  {
    var httpClient = _httpClientFactory.CreateClient();
    //get pad contract

    //send the exectuion request

    //update state management
    await Task.CompletedTask;
  }

   [HttpPost("update")]
   public async Task UpdatePadSataus([FromBody] ExecutionResult result){
    await Task.CompletedTask;
  }

}