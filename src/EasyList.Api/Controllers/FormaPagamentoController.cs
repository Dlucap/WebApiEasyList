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
  // [Authorize]
  [Route("api/[controller]")]
  [ApiController]
  public class FormaPagamentoController : ControllerBase
  {
    private readonly IFormaPagamentoRepository _formaPagamentoRepository;
    private readonly IMapper _mapper;

    public FormaPagamentoController(IFormaPagamentoRepository formaPagamentoRepository,
                                    IMapper mapper)
    {
      _formaPagamentoRepository = formaPagamentoRepository;
      _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<FormaPagamentoApiModel>>> GetFormaPagamento()
    {
      var formaPagamentoApiModel = _mapper.Map<IEnumerable<FormaPagamentoApiModel>>(await _formaPagamentoRepository.ObterTodos());

      if (formaPagamentoApiModel is null)
        return NotFound();

      return Ok(formaPagamentoApiModel);
    }
     
    [HttpGet("{id}")]
    public async Task<ActionResult<FormaPagamentoApiModel>> GetFormaPagamento(Guid id)
    {
      var prodformaPagamentoApiModeluto = await ObterFormaPagamentoPorId(id);

      if (prodformaPagamentoApiModeluto == null) 
        return NotFound();

      return Ok(prodformaPagamentoApiModeluto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutFormaPagamento(Guid id, FormaPagamentoApiModel formaPagamentoApiModel)
    {
      if (id != formaPagamentoApiModel.Id) 
        return BadRequest();

      if (!ModelState.IsValid) 
          BadRequest();

        await _formaPagamentoRepository.Atualizar(_mapper.Map<FormaPagamento>(formaPagamentoApiModel));
     
      return NoContent();
    }
       
    [HttpPost]
    public async Task<ActionResult<FormaPagamentoApiModel>> PostFormaPagamento(FormaPagamentoApiModel formaPagamentoApiModel)
    {
      if (!ModelState.IsValid)
        BadRequest();

      var formaPagamentoEntity = _mapper.Map<FormaPagamento>(formaPagamentoApiModel);

      await _formaPagamentoRepository.Adicionar(formaPagamentoEntity);

      formaPagamentoApiModel.Id = formaPagamentoEntity.Id;

      return CreatedAtAction("GetFormaPagamento", new { id = formaPagamentoApiModel.Id }, formaPagamentoApiModel);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFormaPagamento(Guid id)
    {
      var formaPagamentoApiModel = ObterFormaPagamentoPorId(id);

      if (formaPagamentoApiModel == null) return NotFound();

      await _formaPagamentoRepository.Remover(id);

      return NoContent();
    }

    private async Task<FormaPagamentoApiModel> ObterFormaPagamentoPorId(Guid id)
    {
      return _mapper.Map<FormaPagamentoApiModel>(await _formaPagamentoRepository.ObterPorId(id));
    }
  }
}
