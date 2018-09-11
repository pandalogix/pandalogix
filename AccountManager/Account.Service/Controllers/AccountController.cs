using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace AccountManager
{
    [Route("api/account")]
    public class AccountController : Controller
    {
        private readonly Context.AccountManagerContext context;
        public AccountController(Context.AccountManagerContext context)
        {
            this.context = context;
            ((DbContext)context).ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        [HttpPost]
        [Route("")]
        [ProducesResponseType(201)]
        public async Task<IActionResult> Create([FromBody] Models.Account account)
        {
            this.context.Add(account);
            await this.context.SaveChangesAsync();
            return StatusCode(201);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(204)]
        public async Task<IActionResult> Delete(long id)
        {
            var account = this.context.Accounts.Where(p => p.Id == id).FirstOrDefault();
            if (account == null)
            {
                return NotFound();
            }
            this.context.Accounts.Remove(account);
            await this.context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        [Route("")]
        public async Task<IActionResult> Update([FromBody]  Models.Account account)
        {
            var existing = this.context.Accounts.Where(p => p.Id == account.Id).FirstOrDefault();

            if (existing == null)
                return NotFound();

            this.context.Accounts.Update(account);
            await this.context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(long id)
        {
            var existing = await this.context.Accounts.Where(p => p.Id == id).FirstOrDefaultAsync();

            if (existing == null)
                return NotFound();
            return Ok(existing);
        }

    }
}