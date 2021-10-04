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
  //[Authorize]
  [Route("api/v{version:apiVersion}/[controller]")]
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
    public async Task<ActionResult<IEnumerable<CompraApiModel>>> GetCompra()
    {

      var compras = await _compraRepository.ObterTodasCompras();
      var compra = _mapper.Map<IEnumerable<CompraApiModel>>(compras);

      if (compra == null)
        return NotFound();

      return Ok(compra);
    }


    [HttpGet("valor-total-compra/{id}")]
    public async Task<ActionResult<double>> GetValorTotalCompra(Guid id)
    { 
      var compra = await CalculaValorTotalCompra(id);

      if (compra == null || compra == -1)
        return BadRequest();

      return Ok(compra);
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

      var compraEntity = _mapper.Map<Compra>(compraApiModel);

      await _compraRepository.Adicionar(compraEntity);
      compraApiModel.Id = compraEntity.Id;

      foreach (var item in compraEntity.ItemsCompra)
      {
        item.CompraId = compraEntity.Id; 
        await _itemCompraService.Adicionar(item);
      }     

      return CreatedAtAction("GetCompra", new { id = compraApiModel.Id }, compraApiModel);
    }
  
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompra(Guid id)
    {
      var compraApi = await ObterCompraPorId(id);

      if (compraApi == null)
        return NotFound();

      await _compraRepository.Remover(id);

      return NoContent();
    }

    private async Task<CompraApiModel> ObterCompraPorId(Guid id)
    {
      var compra = await _compraRepository.ObterPorId(id);
      return _mapper.Map<CompraApiModel>(compra);
    }

    private async Task<decimal> CalculaValorTotalCompra(Guid id)
    {
      var valorCompra = await _compraRepository.CalculaValorTotalCompra(id);
      return valorCompra;
    }

  }
}
