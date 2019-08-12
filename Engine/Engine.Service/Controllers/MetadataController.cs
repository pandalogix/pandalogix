using System.Reflection;
using System.Threading.Tasks;
using Engine.Core.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Engine.Service.Controllers
{
  [Route("api/[controller]")]
  public class MetadataController : Controller
  {
    [HttpGet]
    [Route("nodes")]
    public async Task<IActionResult> GetNodesMetaData()
    {
      var result= MetadataHelpers.GetMetadata();
      return await Task.FromResult(Ok(result));
    }
  }
}