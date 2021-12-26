using System.Reflection;
using System.Threading.Tasks;
using Engine.Core.Helpers;
using Microsoft.AspNetCore.Mvc;
using Engine.Contracts;

namespace Engine.Manager.Controllers
{
  [Route("[controller]")]
  public class MetadataController : Controller
  {
    [HttpGet]
    [Route("nodes")]
    public async Task<string> GetNodesMetaData()
    {
      var result= MetadataHelpers.GetMetadata();
      Console.WriteLine(result);
      var s = Newtonsoft.Json.JsonConvert.SerializeObject(result);

      return await Task.FromResult(s);
    }
  }
}