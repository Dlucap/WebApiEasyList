using AutoMapper;
using EasyList.Api.ApiModels;
using EasyList.Business.Interfaces.IRepository;
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
  public class CompraCompartilhadaController : ControllerBase
  {
    private readonly ICompraCompartilhadaRepository _CompraCompartilhadaRepository;
    private readonly IMapper _mapper;

    public CompraCompartilhadaController(ICompraCompartilhadaRepository CompraCompartilhadaRepository, IMapper mapper)
    {
      _CompraCompartilhadaRepository = CompraCompartilhadaRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompraCompartilhadaApiModel>>> GetCompraCompartilhada()
    {
      var compraCompartilhada = _mapper.Map<IEnumerable<CompraCompartilhadaApiModel>>(await _CompraCompartilhadaRepository.ObterTodos());

      if (compraCompartilhada is null)
        return NotFound();

      return Ok(compraCompartilhada);
    }

    
    [HttpGet("{id}")]
    public async Task<ActionResult<CompraCompartilhadaApiModel>> GetCompraCompartilhada(Guid idCompra)
    {
      var compraCompartilhada = await ObterCompraCompartilhadaPorId(idCompra);

      if (compraCompartilhada == null)
        return NotFound();

      return Ok(compraCompartilhada);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompraCompartilhada(Guid id, CompraCompartilhadaApiModel compraCompartilhadaApiModel)
    {
      if (id != compraCompartilhadaApiModel.Id)      
        return BadRequest();

      if (!ModelState.IsValid)
        return BadRequest();

      await _CompraCompartilhadaRepository.Atualizar(_mapper.Map<CompraCompartilhada>(compraCompartilhadaApiModel)); 

      return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<Compra>> PostCompraCompartilhada(CompraCompartilhadaApiModel compraCompartilhadaApiModel)
    {
      if (!ModelState.IsValid)
        return BadRequest();

      await _CompraCompartilhadaRepository.Adicionar(_mapper.Map<CompraCompartilhada>(compraCompartilhadaApiModel));         

      return CreatedAtAction("GetCompraCompartilhada", new { id = compraCompartilhadaApiModel.Id }, compraCompartilhadaApiModel);
    }
        
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompartilhamentoCompra(Guid id)
    {
      var compraCompartilhadaApiModel = await ObterCompraCompartilhadaPorId(id);
      
      if (compraCompartilhadaApiModel == null)
        return NotFound();

      await _CompraCompartilhadaRepository.Remover(id);

      return NoContent();
    }

    private async Task<CompraCompartilhadaApiModel> ObterCompraCompartilhadaPorId(Guid id)
    {
      var compraCompartilhada = _mapper.Map<CompraCompartilhadaApiModel>(await _CompraCompartilhadaRepository.ObterPorId(id));

      return compraCompartilhada;
    }
  
  }
}
