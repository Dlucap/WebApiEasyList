using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiEasyList.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiEasyList.Controllers
{
  //[Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class ItmCompraController : ControllerBase
  {
    private readonly ItmCompraDbContext _context;  

    public ItmCompraController(ItmCompraDbContext context)
    {
      _context = context;
    }
    // GET: api/ItmCompra
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItmCompra>>> GetItmCompra()
    {
      return await _context.ItmCompra.ToListAsync();
    }

    //// GET: api/ItmCompra
    //[HttpGet]
    //public async Task<ActionResult<IEnumerable<ItmCompraList>>> GetItmCompraList()
    //{
    //  return await _context.ItmCompraList.ToListAsync();
    //}

    // GET: api/ItmCompra/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ItmCompra>> GetItmCompra(int id)
    {
      var ItmCompra = await _context.ItmCompra.FindAsync(id);

      if (ItmCompra == null)
      {
        return NotFound();
      }

      return ItmCompra;
    }

    // GET: api/ItmCompra/5
    [HttpGet("{id}/{nomeUser}")]
    public async Task<ActionResult<ItmCompra>> GetItmCompra(int id,string nomeUser)
    {
      var ItmCompra = await _context.ItmCompra.FindAsync(id);

      if (ItmCompra == null || (nomeUser != ItmCompra.RecCreatedBy || nomeUser != ItmCompra.RecModifiedBy))
      {
        return NotFound();
      }

      return ItmCompra;
    }

    // PUT: api/ItmCompra/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutItmCompra(int id, ItmCompra categoria)
    {
      if (id != categoria.Id)
      {
        return BadRequest();
      }

      _context.Entry(categoria).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ItmCompraExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

  
    // POST: api/ItmCompra
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ItmCompraList>> PostItmCompra(ItmCompraList itmCompra)
    {
      foreach (var item in itmCompra)
      {
        _context.ItmCompra.Add(item);
      }

      await _context.SaveChangesAsync();

      return CreatedAtAction("GetItemCompra", itmCompra);
    }

    // DELETE: api/ItmCompra/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItmCompra(int id)
    {
      var compra = await _context.ItmCompra.FindAsync(id);
      if (compra == null)
      {
        return NotFound();
      }

      _context.ItmCompra.Remove(compra);
      await _context.SaveChangesAsync();

      return NoContent();
    }


    private bool ItmCompraExists(int id)
    {
      return _context.ItmCompra.Any(e => e.Id == id);
    }
  }
}
