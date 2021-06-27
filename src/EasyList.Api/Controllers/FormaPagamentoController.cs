using AutoMapper;
using EasyList.Api.ApiModels;
using EasyList.Business.Interfaces;
using EasyList.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

    // GET: api/Compra
    [HttpGet]
    public async Task <IEnumerable<FormaPagamentoApiModel>> GetFormaPagamento()
    {
      var formaPagamentoApiModel = _mapper.Map<IEnumerable<FormaPagamentoApiModel>>(await _formaPagamentoRepository.ObterTodos());

      return formaPagamentoApiModel;
    }

    // GET: api/Compra/5
    [HttpGet("{id}")]
    public async Task<ActionResult<FormaPagamentoApiModel>> GetFormaPagamento(int id)
    {
      var prodformaPagamentoApiModeluto = await _formaPagamentoRepository.ObterFormaPagamentoPorId(id);

      if (prodformaPagamentoApiModeluto == null) return NotFound();

      return Ok(prodformaPagamentoApiModeluto);
    }

    // PUT: api/Compra/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFormaPagamento(int id, FormaPagamentoApiModel formaPagamentoApiModel)
    {
      if (id != formaPagamentoApiModel.Id) return BadRequest();

      if (!ModelState.IsValid) BadRequest();

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

    // POST: api/Compra
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<Compra>> PostFormaPagamento(FormaPagamentoApiModel formaPagamentoApiModel)
    {
      if (!ModelState.IsValid) BadRequest();

      await _formaPagamentoRepository.Adicionar(_mapper.Map<FormaPagamento>(formaPagamentoApiModel));

      return CreatedAtAction("GetProduto", new { id = formaPagamentoApiModel.Id }, formaPagamentoApiModel);
    }

    // DELETE: api/Compra/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFormaPagamento(int id)
    {
      var formaPagamentoApiModel = ObterFormaPagamentoPorId(id);

      if (formaPagamentoApiModel == null) return NotFound();

      await _formaPagamentoRepository.Remover(id);

      return NoContent();
    }

    private bool FormaPagamentoExists(int id)
    {
      return _formaPagamentoRepository.FormaPagamentoExist(id);
    }

    private async Task<ProdutoApiModel> ObterFormaPagamentoPorId(int id)
    {
      return _mapper.Map<ProdutoApiModel>(await _formaPagamentoRepository.ObterFormaPagamentoPorId(id));
    }
  }
}
