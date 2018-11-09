using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;

namespace WebSPA.Controllers
{
  [Route("api/executor")]
  public class ExecutorController : Controller
  {
    private IHttpClientFactory _clientFactory;
    const string APIHEADER = "APIKEY";
    public ExecutorController(IHttpClientFactory clientFactory) => this._clientFactory = clientFactory;
    [HttpPost("")]
    public async Task<IActionResult> Execute([FromQuery]string padIdentifier, [FromBody] string triggerObject)
    {
      StringValues header;
      if (this.Request.Headers.TryGetValue(APIHEADER, out header))
      {
        if (header.Count == 0) return new StatusCodeResult(401);
        var apikey = header[0];
        using (var accountMgr = this._clientFactory.CreateClient("accountMgr"))
        {
          var acctRespone = await accountMgr.GetAsync($"api/account/pad?identifier={padIdentifier}&apikey={apikey}");
          if (acctRespone.IsSuccessStatusCode)
          {
            using (var padmgr = this._clientFactory.CreateClient("padMgr"))
            {
              var padstring = await padmgr.GetStringAsync($"api/pad/?identifier={padIdentifier}");
              if (!string.IsNullOrEmpty(padstring))
              {
                var pad = Newtonsoft.Json.JsonConvert.DeserializeObject<Engine.Contracts.PadContract>(padstring);
                using (var engineMgr = this._clientFactory.CreateClient("engineMgr"))
                {
                  var mappings = new[]{new Engine.Contracts.InstanceMapping()
                                    {
                                    NodeId=1,
                                    FieldMappings = new List<Engine.Contracts.FieldMapping>()
                                    {
                                        new Engine.Contracts.FieldMapping(){ FieldName = "JsonString", Value=triggerObject },
                                        new Engine.Contracts.FieldMapping(){ FieldName = "Schema", Value=pad.TriggerData }
                                    }
                                    }};
                  var instance = new Engine.Contracts.Instances(mappings);
                  //create execution instance
                  PadExecutionEvent executionEvent = new PadExecutionEvent
                  {
                    Pad = pad,
                    Instances = instance
                  };
                  var engineRespone = await engineMgr.PostAsJsonAsync<PadExecutionEvent>("api/engine", executionEvent);
                  if (engineRespone.IsSuccessStatusCode)
                  {
                    return new OkResult();
                  }
                  else
                  {
                    return StatusCode(500);
                  }
                }
              }
            }
          }
          return BadRequest();
        }
      }
      return new StatusCodeResult(401);
    }

  }
}


