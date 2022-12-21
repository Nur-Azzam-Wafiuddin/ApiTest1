using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ApiTest1.Model;

namespace ApiTest1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly DataContext dataContext;

        public AccountController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpGet]
        public async Task<ActionResult<List<Account>>> Get()
        {
            return Ok(await dataContext.Accounts.Include(e => e.productHistory).ToArrayAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Account>>> GetbyId(int id)
        {
            var Account = await dataContext.Accounts.Include(e => e.productHistory).SingleOrDefaultAsync(p => p.id == id); //.FindAsync(id)
            if (Account == null)
            {
                return BadRequest("Account not found");
            }
            return Ok(Account);
        }

        [HttpPost]
        public async Task<ActionResult<List<Account>>> AddAccount(Account Account)
        {
            dataContext.Accounts.Add(Account);
            await dataContext.SaveChangesAsync();
            return Ok(await dataContext.Accounts.Include(e => e.productHistory).ToListAsync());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<List<Account>>> UpdateAccountbyId(Account Account, int id)
        {
            var updateAccount = await dataContext.Accounts.Include(e => e.productHistory).SingleOrDefaultAsync(p => p.id == id);
            if (updateAccount == null)
            {
                return BadRequest("Account not found");
            }

            updateAccount.Name = Account.Name;
            updateAccount.username = Account.username;
            updateAccount.password = Account.password;
            updateAccount.productHistory = Account.productHistory;

            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.Accounts.Include(e => e.productHistory).ToListAsync());
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Account>>> DeletebyId(int id)
        {
            var deleteAccount = await dataContext.Accounts.Include(e => e.productHistory).SingleOrDefaultAsync(p => p.id == id);
            if (deleteAccount == null)
            {
                return BadRequest("Account not found");
            }
            dataContext.Accounts.Remove(deleteAccount);
            await dataContext.SaveChangesAsync();

            return Ok(await dataContext.Accounts.Include(e => e.productHistory).ToListAsync());
        }
    }
}
