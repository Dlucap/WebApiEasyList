using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiEasyList.Data;

namespace WebApiEasyList.Controllers
{
  //[Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class CompraCompartilhadaController : ControllerBase
  {
    private readonly CompraCompartilhadaDbContext _context;

    public CompraCompartilhadaController(CompraCompartilhadaDbContext context)
    {
      _context = context;
    }

    // GET: api/Compra
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompraCompartilhada>>> GetCompraCompartilhada()
    {
      return await _context.CompraCompartilhada.ToListAsync();
    }

    // GET: api/Compra/5
    [HttpGet("{idCompra}")]
    public async Task<ActionResult<CompraCompartilhada>> GetCompraCompartilhada(int idCompra)
    {
      var compra = await _context.CompraCompartilhada.FindAsync(idCompra);

      if (compra == null)
      {
        return NotFound();
      }

      return compra;
    }

    // PUT: api/Compra/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompraCompartilhada(int id, CompraCompartilhada compraCompartilhada)
    {
      if (id != compraCompartilhada.Id)
      {
        return BadRequest();
      }

      _context.Entry(compraCompartilhada).State = EntityState.Modified;

      try
      {
        await _context.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!CompraExists(id))
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

    // POST: api/Compra
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Compra>> PostCompraCompartilhada(CompraCompartilhada compraCompartilhada )
    {
      _context.CompraCompartilhada.Add(compraCompartilhada);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetCompraCompartilhada", new { id = compraCompartilhada.Id }, compraCompartilhada);
    }

    // DELETE: api/Compra/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompartilhamentoCompra(int id)
    {
      var compartilhamentoCompra = await _context.CompraCompartilhada.FindAsync(id);
      if (compartilhamentoCompra == null)
      {
        return NotFound();
      }

      _context.CompraCompartilhada.Remove(compartilhamentoCompra);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool CompraExists(int id)
    {
      return _context.CompraCompartilhada.Any(e => e.Id == id);
    }
  }
}
