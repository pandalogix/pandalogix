using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebSPA.Controllers
{
  [Route("api/[controller]")]
  public class PadController : Controller
  {
    private IHttpClientFactory _clientFactory;
    private const string clientName = "padMgr";
    public PadController(IHttpClientFactory clientFactory)
    {
      this._clientFactory = clientFactory;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Create([FromBody]Pad pad)
    {
      using (var client = this._clientFactory.CreateClient(clientName))
      {
        var response = await client.PostAsJsonAsync<Pad>("/api/pad", pad);
        if (!response.IsSuccessStatusCode)
        {
          return StatusCode((int)System.Net.HttpStatusCode.InternalServerError);
        }
        else
        {
          var userCreated = await response.Content.ReadAsAsync<Pad>();
          // this.SetCookie(user);

          return Created("", userCreated);
        }
      }
    }


    [HttpPut]
    [Route("")]
    public async Task<IActionResult> Update(Pad pad)
    {
      using (var client = this._clientFactory.CreateClient(clientName))
      {
        var response = await client.PutAsJsonAsync<Pad>("/api/pad", pad);
        if (!response.IsSuccessStatusCode)
        {
          return StatusCode((int)System.Net.HttpStatusCode.InternalServerError);
        }
        else
        {
          return NoContent();
        }
      }
    }

    [HttpDelete]
    [Route("")]
    public async Task<IActionResult> Delete(long id)
    {
      using (var client = this._clientFactory.CreateClient(clientName))
      {
        var response = await client.DeleteAsync($"/api/pad/{id}");
        if (!response.IsSuccessStatusCode)
        {
          return StatusCode((int)System.Net.HttpStatusCode.InternalServerError);
        }
        else
        {
          return NoContent();
        }
      }
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Get(long id)
    {

      using (var client = this._clientFactory.CreateClient(clientName))
      {
        var response = await client.GetAsync($"/api/pad/{id}");
        if (!response.IsSuccessStatusCode)
        {
          return StatusCode((int)System.Net.HttpStatusCode.InternalServerError);
        }
        else
        {
          var pad = await response.Content.ReadAsAsync<Pad>();
          return Ok(pad);
        }
      }
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetPads(int page, int pageSize = 25)
    {
      using (var client = this._clientFactory.CreateClient(clientName))
      {
        var response = await client.GetAsync($"/api/pad?page={page}&pageSize={pageSize}");
        if (!response.IsSuccessStatusCode)
        {
          return StatusCode((int)System.Net.HttpStatusCode.InternalServerError);
        }
        else
        {
          var pads = await response.Content.ReadAsAsync<PagedResult<Pad>>();
          return Ok(pads);
        }
      }
    }

    public class PagedResult<T>
    {
        public int count { get; set; }
        public IEnumerable<T> data { get; set; }
    }
  }
}