using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyList.Api.V2.Controllers
{
#if !DEBUG
  [Authorize]
#endif
  [ApiController]
  [ApiVersion("2.0")]
  [Route("api/v{version:apiVersion}/[controller]")]
  
  public class EstoqueController : ControllerBase
  {

    /// <summary>
    ///  Retorna compras cadastrados no banco
    /// </summary>  
    /// <response code="200"> Sucesso </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpGet]
    public async Task<ActionResult> GetCompra()
    {
       return Ok("Olá");
    }
  }
}
