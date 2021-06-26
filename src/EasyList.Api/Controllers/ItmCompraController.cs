using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyList.Api.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EasyList.Api.Controllers
{
  //[Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class ItmCompraController : ControllerBase
  {
    private readonly ItmCompraDbContext _contextItem;

    public ItmCompraController(ItmCompraDbContext contextItem)
    {
      _contextItem = contextItem;     
    }

    // GET: api/ItmCompra
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ItmCompra>>> GetItmCompra()
    {
      return await _contextItem.ItmCompra.ToListAsync();
    }

    // GET: api/ItmCompra/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ItmCompra>> GetItmCompra(int id)
    {
      var ItmCompra = await _contextItem.ItmCompra.FindAsync(id);

      if (ItmCompra == null)
      {
        return NotFound();
      }

      return ItmCompra;
    }

    // PUT: api/ItmCompra/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutItmCompra(int id, ItmCompra itmCompra)
    {
      if (id != itmCompra.Id)
      {
        return BadRequest();
      }

      if (!ItmCompraExists(id))
      {
        return NotFound();
      }

      _contextItem.Entry(itmCompra).State = EntityState.Modified;

      try
      {
        await _contextItem.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        throw;
      }

      return NoContent();
    }

    // POST: api/ItmCompra
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ItmCompraList>> PostItmCompra(ItmCompraList itmCompra)
    {
      //todo: Validar se a compra ja está finalizada e se ela existe
      foreach (var item in itmCompra)
      {       
        _contextItem.ItmCompra.Add(item);
      }

      await _contextItem.SaveChangesAsync();

      return CreatedAtAction("GetItmCompra", itmCompra);
    }

    // DELETE: api/ItmCompra/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItmCompra(int id)
    {
      var compra = await _contextItem.ItmCompra.FindAsync(id);
      if (compra == null)
      {
        return NotFound();
      }

      _contextItem.ItmCompra.Remove(compra);
      await _contextItem.SaveChangesAsync();

      return NoContent();
    }

    private bool ItmCompraExists(int id)
    {
      return _contextItem.ItmCompra.Any(e => e.Id == id);
    }
  }
}
