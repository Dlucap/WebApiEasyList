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
 // [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class FormaPagamentoController : ControllerBase
  {
    private readonly FormaPagamentoDbContext _context;

    public FormaPagamentoController(FormaPagamentoDbContext context)
    {
      _context = context;
    }

    // GET: api/Compra
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FormaPagamento>>> GetFormaPagamento()
    {
      return await _context.FormaPagamento.ToListAsync();
    }

    // GET: api/Compra/5
    [HttpGet("{id}")]
    public async Task<ActionResult<FormaPagamento>> FormaPagamento(int id)
    {
      var formaPag = await _context.FormaPagamento.FindAsync(id);

      if (formaPag == null)
      {
        return NotFound();
      }

      return formaPag;
    }

    // PUT: api/Compra/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompra(int id, FormaPagamento formaPagamento)
    {
      if (id != formaPagamento.Id)
      {
        return BadRequest();
      }

      _context.Entry(formaPagamento).State = EntityState.Modified;

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
    public async Task<ActionResult<Compra>> PostCompra(FormaPagamento formaPagamento)
    {
      _context.FormaPagamento.Add(formaPagamento);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetCompra", new { id = formaPagamento.Id
                                              }, formaPagamento);
    }

    // DELETE: api/Compra/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompra(int id)
    {
      var compra = await _context.FormaPagamento.FindAsync(id);
      if (compra == null)
      {
        return NotFound();
      }

      _context.FormaPagamento.Remove(compra);
      await _context.SaveChangesAsync();

      return NoContent();
    }

    private bool CompraExists(int id)
    {
      return _context.FormaPagamento.Any(e => e.Id == id);
    }
  }
}
