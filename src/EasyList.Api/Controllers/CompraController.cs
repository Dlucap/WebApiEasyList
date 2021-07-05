using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasyList.Api.Data;
using EasyList.Business.Models;

namespace EasyList.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CompraController : ControllerBase
  {
    private readonly ICompraRepository _compraRepository;

    public CompraController(CompraDbContext context)
    {
      _context = context;
    }

    // GET: api/Compra
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompraApiModels>>> GetCompra()
    {
      return await _context.Compra.ToListAsync();
    }

    // GET: api/Compra/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Compra>> GetCompra(Guid id)
    {
      var compra = await _context.Compra.FindAsync(id);

      if (compra == null)
      {
        return NotFound($"Compra {id}, não encontrado na base de dados.");
      }

      return compra;
    }

    // PUT: api/Compra/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompra(Guid id, Compra compra)
    {
      if (id != compra.Id)
        return BadRequest();

      if (CompraExists(id))
        return NotFound();

      if (compra.StatusCompra == StatusCompraEnum.CompraFinalizada)
        return BadRequest("Compra já finaloizada.");

      _context.Entry(compra).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();      
      }
      catch (DbUpdateConcurrencyException)
      {
        throw;
      }

      return NoContent();
    }

    // POST: api/Compra
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Compra>> PostCompra(Compra compra)
    {
      _context.Compra.Add(compra);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetCompra", new { id = compra.Id }, compra);
    }

    // DELETE: api/Compra/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompra(Guid id)
    {
      var compra = await _context.Compra.FindAsync(id);
      if (compra == null)
      {
        return NotFound();
      }

      _context.Compra.Remove(compra);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool CompraExists(Guid id)
    {
      return _context.Compra.Any(e => e.Id == id);
    }
  }
}
