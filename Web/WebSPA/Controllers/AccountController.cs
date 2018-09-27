using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace WebSPA.Controllers
{
  // [Authorize]

  [Route("api/[controller]")]
  public class AccountController : Controller
  {
    private IHttpClientFactory _clientFactory;

    public AccountController(IHttpClientFactory clientFactory)
    {
      this._clientFactory = clientFactory;
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> Create([FromBody]User user)
    {
      using (var client = this._clientFactory.CreateClient("accountMgr"))
      {
        var response = await client.PostAsJsonAsync<User>("/api/account", user);
        if (!response.IsSuccessStatusCode)
        {
          return StatusCode((int)System.Net.HttpStatusCode.InternalServerError);
        }
        else
        {
          var userCreated = await response.Content.ReadAsAsync<User>();
          // this.SetCookie(user);

          return Created("", userCreated);
        }
      }
    }

    [HttpPut]
    [Route("")]
    public async Task<IActionResult> Update(User user)
    {
      using (var client = this._clientFactory.CreateClient("accountMgr"))
      {
        var response = await client.PutAsJsonAsync<User>("/api/account", user);
        if (response.IsSuccessStatusCode)
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
      using (var client = this._clientFactory.CreateClient("accountMgr"))
      {
        var response = await client.DeleteAsync($"/api/account/{id}");
        if (response.IsSuccessStatusCode)
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

      using (var client = this._clientFactory.CreateClient("accountMgr"))
      {
        var response = await client.GetAsync($"/api/account/{id}");
        if (!response.IsSuccessStatusCode)
        {
          return StatusCode((int)System.Net.HttpStatusCode.InternalServerError);
        }
        else
        {
          var user = await response.Content.ReadAsAsync<User>();
          return Ok(user);
        }
      }
    }
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetByEmail([FromQuery]string email)
    {
      using (var client = this._clientFactory.CreateClient("accountMgr"))
      {
        var response = await client.GetAsync($"/api/account?email={email}");
        if (!response.IsSuccessStatusCode)
        {
          return StatusCode((int)System.Net.HttpStatusCode.InternalServerError);
        }
        else
        {
          var user = await response.Content.ReadAsAsync<User>();
          // this.SetCookie(user);
          return Ok(user);
        }
      }
    }

    private void SetCookie(User user)
    {
      if (this.Request.Headers.ContainsKey("X-Expire"))
      {
        this.Response.Cookies.Append("user",
        Newtonsoft.Json.JsonConvert.SerializeObject(user),
        new Microsoft.AspNetCore.Http.CookieOptions()
        {
          Expires = DateTimeOffset.FromUnixTimeMilliseconds(Convert.ToInt64(this.Request.Headers["X-Expire"].FirstOrDefault())),
          HttpOnly = true
        });
      }
    }
  }
}