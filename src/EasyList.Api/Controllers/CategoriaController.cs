using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyList.Api.Data;
using EasyList.Api.ApiModels;
using System;
using EasyList.Business.Interfaces;

namespace EasyList.Api.Controllers
{
  //[Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class CategoriaController : ControllerBase
  {
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaController(CategoriaDbContext context)
    {
      _context = context;
    }
    // GET: api/Categoria
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaApiModel>>> GetCategoria()
    {
      return await _context.Categoria.ToListAsync();
    }

    // GET: api/Categoria/5
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaApiModel>> GetCategoria(Guid id)
    {
      var compra = await _context.Categoria.FindAsync(id);

      if (compra == null)
      {
        return NotFound();
      }

      return compra;
    }

    // PUT: api/Categoria/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategoria(Guid id, CategoriaApiModel categoria)
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
        if (!CategoriaExists(id))
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
    public async Task<ActionResult<CategoriaApiModel>> PostCategoria(CategoriaApiModel categoria)
    {
      _context.Categoria.Add(categoria);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetCategoria", new { id = categoria.Id }/*, categoria*/);
    }

    // DELETE: api/Categoria/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoria(Guid id)
    {
      var compra = await _context.Categoria.FindAsync(id);
      if (compra == null)
      {
        return NotFound();
      }

      _context.Categoria.Remove(compra);
      await _context.SaveChangesAsync();

      return NoContent();
    }


    private bool CategoriaExists(Guid id)
    {
      return _context.Categoria.Any(e => e.Id == id);
    }

  }
}