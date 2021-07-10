using AutoMapper;
using EasyList.Api.ApiModels;
using EasyList.Business.Interfaces;
using EasyList.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

    // GET: api/FormaPagamento
    [HttpGet]
    public async Task<IEnumerable<FormaPagamentoApiModel>> GetFormaPagamento()
    {
      var formaPagamentoApiModel = _mapper.Map<IEnumerable<FormaPagamentoApiModel>>(await _formaPagamentoRepository.ObterTodos());

      return formaPagamentoApiModel;
    }

    // GET: api/FormaPagamento/5
    [HttpGet("{id}")]
    public async Task<ActionResult<FormaPagamentoApiModel>> GetFormaPagamento(Guid id)
    {
      var prodformaPagamentoApiModeluto = await _formaPagamentoRepository.ObterFormaPagamentoPorId(id);

      if (prodformaPagamentoApiModeluto == null) 
        return NotFound();

      return Ok(prodformaPagamentoApiModeluto);
    }

    // PUT: api/FormaPagamento/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFormaPagamento(Guid id, FormaPagamentoApiModel formaPagamentoApiModel)
    {
      if (id != formaPagamentoApiModel.Id) 
        return BadRequest();

      if (!ModelState.IsValid) 
          BadRequest();

      try
      {
        await _formaPagamentoRepository.Atualizar(_mapper.Map<FormaPagamento>(formaPagamentoApiModel));
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!FormaPagamentoExists(id))
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


    //POST: api/FormaPagamento
    //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<FormaPagamentoApiModel>> PostFormaPagamento(FormaPagamentoApiModel formaPagamentoApiModel)
    {
      if (!ModelState.IsValid)
        BadRequest();

      await _formaPagamentoRepository.Adicionar(_mapper.Map<FormaPagamento>(formaPagamentoApiModel));

      return CreatedAtAction("GetProduto", new { id = formaPagamentoApiModel.Id }, formaPagamentoApiModel);
    }

    // DELETE: api/FormaPagamento/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFormaPagamento(Guid id)
    {
      var formaPagamentoApiModel = ObterFormaPagamentoPorId(id);

      if (formaPagamentoApiModel == null) return NotFound();

      await _formaPagamentoRepository.Remover(id);

      return NoContent();
    }

    private bool FormaPagamentoExists(Guid id)
    {
      return _formaPagamentoRepository.FormaPagamentoExist(id);
    }

    private async Task<FormaPagamentoApiModel> ObterFormaPagamentoPorId(Guid id)
    {
      return _mapper.Map<FormaPagamentoApiModel>(await _formaPagamentoRepository.ObterFormaPagamentoPorId(id));
    }
  }
}
