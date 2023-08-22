using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ContentManagementApi;

[ApiController]
public class AccountsController : ControllerBase
{
    private readonly BlogsContext _context;

    public AccountsController(BlogsContext context)
    {
        _context = context;
    }

    [HttpGet("api/accounts")]
    public async Task<IEnumerable<Account>> GetAccounts()
    {
        return await _context.Accounts
            .AsNoTracking()
            .ToListAsync();
    }

    [HttpGet("api/accounts/{id}")]
    public async Task<ActionResult<Post>> GetAccount(int id)
    {
        var account = await _context.Accounts
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == id);

        return account == null ? NotFound() : Ok(account);
    }
    
    [HttpPut("api/accounts")]
    public async Task<ActionResult<Post>> UpdateAccount([FromBody] Account account)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        Account? existingAccount = await _context.Accounts.Where(a => a.Id == account.Id).FirstOrDefaultAsync();

        if (existingAccount == null)
        {
            return NotFound();
        }

        _context.Entry(existingAccount).CurrentValues.SetValues(account);

        await _context.SaveChangesAsync();

        return Ok(account);
    }
}
