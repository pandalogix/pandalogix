using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using PadManager.Core.Models;
using PadManager.Core;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace PadManager.Service.Controllers
{
  [Route("api/pad")]

  public class PadManagerController : Controller
  {
    private readonly PandaManagerContext context;

    public PadManagerController(PandaManagerContext context)
    {
      this.context = context;
      ((DbContext)context).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;

    }
    [HttpPost]
    [Route("")]
    [ProducesResponseType(201)]
    public async Task<IActionResult> Create([FromBody] Engine.Contracts.PadContract pad, [FromQuery]Guid user)
    {
      var model = pad.ToModel();
      this.context.Add(model);

      this.context.SaveChanges();
      this.context.Add(new PadManager.Core.Models.AccountPad()
      {
        UserId = user,
        PadId = model.Identifier
      });
      this.context.SaveChanges();

      return await Task.FromResult(Ok(model.ToContract()));
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(204)]
    public async Task<IActionResult> Delete(long id)
    {
      var pad = this.context.Pads.Where(p => p.Id == id).FirstOrDefault();
      if (pad == null)
      {
        return NotFound();
      }
      this.context.Pads.Remove(pad);
      this.context.SaveChanges();
      return await Task.FromResult(NoContent());
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody]  Engine.Contracts.PadContract pad)
    {
      var existingpad = this.context.Pads.Where(p => p.Id == pad.Id).FirstOrDefault();

      if (existingpad == null)
        return NotFound();
      var model = pad.ToModel();
      this.context.Pads.Update(model);
      this.context.SaveChanges();
      return await Task.FromResult(Ok(model.ToContract()));
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> GetPad(long id)
    {
      var existingpad = await this.context.Pads.Where(p => p.Id == id).FirstOrDefaultAsync();

      if (existingpad == null)
        return NotFound();
      return Ok(existingpad.ToContract());
    }


    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetPad([FromQuery]Guid identifier)
    {
      var existingpad = await this.context.Pads.
          Where(p => p.Identifier == identifier)
          .Include(p => p.Nodes)
          .FirstOrDefaultAsync();

      if (existingpad == null)
        return NotFound();
      return Ok(existingpad.ToContract());
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetPads(int page, int pageSize)
    {
      var pads = await this.context.Pads
                     .OrderBy(p => p.LastUpdatedDate)
                     .Skip((page - 1) * pageSize)
                     .Take(pageSize)
                     .ToListAsync();
      var count = await this.context.Pads.LongCountAsync();

      return Ok(new { count = count, data = pads.Select(p => p.ToContract()) });

    }
  }
}