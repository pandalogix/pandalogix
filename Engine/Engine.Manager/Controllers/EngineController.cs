namespace Engine.Manager.Controllers;

[ApiController]
[Route("[controller]")]
public class EngineController : ControllerBase
{
  private ILogger<EngineController> _logger;
  public EngineController(ILogger<EngineController> logger)
  {
      _logger=logger;
  }
}