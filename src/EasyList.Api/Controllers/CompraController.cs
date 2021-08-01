using AutoMapper;
using EasyList.Api.ApiModels;
using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CompraController : ControllerBase
  {
    private readonly ICompraRepository _compraRepository;
    private readonly IItmCompraService _itemCompraService;
    private readonly IMapper _mapper;

    public CompraController(ICompraRepository compraRepository, IItmCompraService itemCompraService, IMapper mapper)
    {
      _compraRepository = compraRepository;
      _itemCompraService = itemCompraService;
      _mapper = mapper;
    }
       
    [HttpGet]
    public async Task<IEnumerable<CompraApiModel>> GetCompra()
    {
      var compra = _mapper.Map<IEnumerable<CompraApiModel>>(await _compraRepository.ObterTodos());
      
      return compra;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Compra>> GetCompra(Guid id)
    {
      var compra = await ObterCompraPorId(id);

      if (compra == null)
        return NotFound();

      return Ok(compra);     
    }  

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompra(Guid id, CompraApiModel compraApiModel)
    {
      if (id != compraApiModel.Id)
        return BadRequest();

      if (!ModelState.IsValid)
        return BadRequest();

      await _compraRepository.Atualizar(_mapper.Map<Compra>(compraApiModel));

      return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Compra>> PostCompra(CompraApiModel compraApiModel)
    {      
      if (!ModelState.IsValid)
        return BadRequest();


      await _compraRepository.Adicionar(_mapper.Map<Compra>(compraApiModel));

      return NoContent();
    }

    // DELETE: api/Compra/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompra(Guid id)
    {
      var compraApi = await ObterCompraPorId(id);

      if (compraApi == null)
        return NotFound();

      await _compraRepository.Remover(id);

      return NoContent();
    }

    private async Task<IEnumerable<CompraApiModel>> ObterCompraPorId(Guid id)
    {
      return _mapper.Map<IEnumerable<CompraApiModel>>(await _compraRepository.ObterPorId(id));
    }
  }
}
