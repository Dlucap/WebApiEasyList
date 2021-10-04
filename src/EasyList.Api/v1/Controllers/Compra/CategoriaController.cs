using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiEasyList.Data;

namespace WebApiEasyList.Controllers
{
  //[Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class CategoriaController : ControllerBase
  {
    private readonly CategoriaDbContext _context;

    public CategoriaController(CategoriaDbContext context)
    {
      _context = context;
    }
    // GET: api/Categoria
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Categoria>>> GetCategoria()
    {
      return await _context.Categoria.ToListAsync();
    }

    // GET: api/Categoria/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Categoria>> GetCategoria(int id)
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
    public async Task<IActionResult> PutCategoria(int id, Categoria categoria)
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
    public async Task<ActionResult<Compra>> PostCategoria(Categoria categoria)
    {
      _context.Categoria.Add(categoria);
      await _context.SaveChangesAsync();

      return CreatedAtAction("GetCategoria", new { id = categoria.Id }/*, categoria*/);
    }

    // DELETE: api/Categoria/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoria(int id)
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


    private bool CategoriaExists(int id)
    {
      return _context.Categoria.Any(e => e.Id == id);
    }

  }
}
/**
 * System.InvalidOperationException: No route matches the supplied values.
   at Microsoft.AspNetCore.Mvc.CreatedAtActionResult.OnFormatting(ActionContext context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ObjectResultExecutor.ExecuteAsyncCore(ActionContext context, ObjectResult result, Type objectType, Object value)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ObjectResultExecutor.ExecuteAsync(ActionContext context, ObjectResult result)
   at Microsoft.AspNetCore.Mvc.ObjectResult.ExecuteResultAsync(ActionContext context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.InvokeResultAsync(IActionResult result)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.ResultNext[TFilter,TFilterAsync](State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.InvokeNextResultFilterAsync[TFilter,TFilterAsync]()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.Rethrow(ResultExecutedContextSealed context)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.ResultNext[TFilter,TFilterAsync](State& next, Scope& scope, Object& state, Boolean& isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.InvokeResultFilters()
--- End of stack trace from previous location ---
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeFilterPipelineAsync>g__Awaited|19_0(ResourceInvoker invoker, Task lastTask, State next, Scope scope, Object state, Boolean isCompleted)
   at Microsoft.AspNetCore.Mvc.Infrastructure.ResourceInvoker.<InvokeAsync>g__Awaited|17_0(ResourceInvoker invoker, Task task, IDisposable scope)
   at Microsoft.AspNetCore.Routing.EndpointMiddleware.<Invoke>g__AwaitRequestTask|6_0(Endpoint endpoint, Task requestTask, ILogger logger)
   at Microsoft.AspNetCore.Authentication.AuthenticationMiddleware.Invoke(HttpContext context)
   at Swashbuckle.AspNetCore.SwaggerUI.SwaggerUIMiddleware.Invoke(HttpContext httpContext)
   at Swashbuckle.AspNetCore.Swagger.SwaggerMiddleware.Invoke(HttpContext httpContext, ISwaggerProvider swaggerProvider)
   at Microsoft.AspNetCore.Diagnostics.DeveloperExceptionPageMiddleware.Invoke(HttpContext context)

HEADERS
=======
Accept: 

Accept - Encoding: gzip, deflate, br
Cache-Control: no - cache
Connection: keep - alive
Content - Length: 216
Content - Type: application / json
Host: localhost: 44389
User - Agent: PostmanRuntime / 7.26.10
Postman - Token: 9e264db4 - e981 - 4847 - 9003 - 7968fd83a230

  */