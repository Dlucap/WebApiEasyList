using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyList.Api.Data;
using EasyList.Business.Interfaces.IServices;
using AutoMapper;
using EasyList.Api.ApiModels;
using System;
using EasyList.Business.Models;
using EasyList.Business.Interfaces.IRepository;

namespace EasyList.Api.Controllers
{
  //[Authorize]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  public class ItmCompraController : ControllerBase
  {
    private readonly IItmCompraService _itmCompraService;
    private readonly IItmCompraRepository _itmCompraRepository;
    private readonly IMapper _mapper;

    public ItmCompraController(IItmCompraService itmCompraService, IItmCompraRepository itmCompraRepository, IMapper mapper)
    {
      _itmCompraService = itmCompraService;
      _itmCompraRepository = itmCompraRepository;
      _mapper = mapper;
    }
            
    [HttpGet("{id}")]
    public async Task<ActionResult<ItmCompraApiModel>> GetItmCompra(Guid idCompra, string user)
    {
      var itmCompra = await ObterTodosItensCompra(idCompra, user);

      if (!itmCompra.Any())
        return NotFound();

      return Ok(itmCompra);
    }      

    [HttpPut("{id}")]
    public async Task<IActionResult> PutItmCompra(Guid id, ItmCompraApiModel itemCompraApiModel)
    {
      if (id != itemCompraApiModel.Id)
        return BadRequest();

      if (!ModelState.IsValid)
        return BadRequest();

      await _itmCompraService.Atualizar(_mapper.Map<ItmCompra>(itemCompraApiModel));

      return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<ItmCompra>> PostItmCompra(IEnumerable<ItmCompraApiModel> itemCompraApiModel)
    {
      if (!ModelState.IsValid)
        return BadRequest();

      foreach (var item in itemCompraApiModel)
      {
        await _itmCompraService.Adicionar(_mapper.Map<ItmCompra>(item));

      }

      return NoContent();
    }

    // DELETE: api/ItmCompra/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItmCompra(Guid id)
    {
      var itemCompra = await ObterItemCompraPorId(id);

      if (itemCompra == null)
        return NotFound();

      await _itmCompraService.Remover(id);

      return NoContent();
    }

    private async Task<ItmCompraApiModel> ObterItemCompraPorId(Guid id)
    {
      return _mapper.Map<ItmCompraApiModel>(await _itmCompraService.ObterPorId(id));
    }

    private async Task<IEnumerable<ItmCompraApiModel>> ObterTodosItensCompra(Guid idCompra, string userName)
    {
      var listaItensDaCompra = await _itmCompraService.BuscarItensCompra(idCompra, userName);

      return _mapper.Map<IEnumerable<ItmCompraApiModel>>(listaItensDaCompra);
    }
  }
}
