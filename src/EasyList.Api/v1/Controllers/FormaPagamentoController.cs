using AutoMapper;
using EasyList.Api.ApiModels;
using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyList.Api.Controllers
{
  // [Authorize]
  [Route("api/v{version:apiVersion}/[controller]")]
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

    /// <summary>
    /// Retorna todas as formas de pagamento cadastradas ativas e não ativas.
    /// </summary>
    /// <returns> retorna todas as formas de pagamento cadastradas ativas e não ativas.</returns>
    /// <response code="200"> sucesso maluco </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<FormaPagamentoApiModel>>> GetFormaPagamento()
    {
      var formaPagamentoApiModel = _mapper.Map<IEnumerable<FormaPagamentoApiModel>>(await _formaPagamentoRepository.ObterTodos());

      if (formaPagamentoApiModel is null)
        return NotFound();

      return Ok(formaPagamentoApiModel);
    }

    /// <summary>
    /// Retorna todas as formas de pagamento cadastradas ativas e não ativas por id
    /// </summary>
    /// <returns> retorna todas as formas de pagamento cadastradas ativas e não ativas.</returns>
    /// <response code="200"> sucesso maluco </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpGet("{id}")]
    public async Task<ActionResult<FormaPagamentoApiModel>> GetFormaPagamento(Guid id)
    {
      var prodformaPagamentoApiModeluto = await ObterFormaPagamentoPorId(id);

      if (prodformaPagamentoApiModeluto == null) 
        return NotFound();

      return Ok(prodformaPagamentoApiModeluto);
    }

    /// <summary>
    /// Retorna as formas de pagamento cadastrados no banco
    /// </summary>
    /// <param name="pagina"> Página </param>
    /// <param name="tamanho"> Quantidade de registros por página </param>
    /// <param name="ativo">Ativo 1 / Inativo 0 </param>
    /// <response code="200"> Sucesso </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpGet("{pagina}/{tamanho}/{ativo}")]
    public async Task<ActionResult<FornecedorApiModel>> GetFormaPagamento(int? pagina, int tamanho, bool ativo)
    {
      var fornecedores = await ObterAllFormaPgamento(pagina, tamanho, ativo);

      if (fornecedores == null)
        return NotFound();

      return Ok(fornecedores);
    }

    /// <summary>
    /// Insere o Forma de pagamento
    /// </summary>
    /// <param name="formaPagamentoApiModel"></param>
    /// <returns></returns>
    /// <response code="201"> Criado com Sucesso </response>
    /// <response code="400"> Requisição Inválida </response>
    [HttpPost]
    public async Task<ActionResult<FormaPagamentoApiModel>> PostFormaPagamento(FormaPagamentoApiModel formaPagamentoApiModel)
    {
      if (!ModelState.IsValid)
        BadRequest();

      var formaPagamentoEntity = _mapper.Map<FormaPagamento>(formaPagamentoApiModel);

      await _formaPagamentoRepository.Adicionar(formaPagamentoEntity);

      formaPagamentoApiModel.Id = formaPagamentoEntity.Id;

      return CreatedAtAction("GetFormaPagamento", new { id = formaPagamentoApiModel.Id }, formaPagamentoEntity);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="formaPagamentoApiModel"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFormaPagamento(Guid id, FormaPagamentoApiModel formaPagamentoApiModel)
    {
      if (id != formaPagamentoApiModel.Id) 
        return BadRequest();

      if (!ModelState.IsValid) 
          BadRequest();

      if (!await FormaPagamentoExists(id))
        return NotFound();

      await _formaPagamentoRepository.Atualizar(_mapper.Map<FormaPagamento>(formaPagamentoApiModel));
     
      return NoContent();
    }

    /// <summary>
    /// Atualização Parcial FormaPagamento
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patchDocument"></param>
    /// <returns></returns>
    /// <response code="204"> Atualizado Com Sucesso </response>
    /// <response code="400"> Requisição Inválida </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchFornecedor(Guid id, JsonPatchDocument<FormaPagamentoApiModel> patchDocument)
    {
      if (patchDocument == null)
        return BadRequest();

      if (!ModelState.IsValid)
        return BadRequest();

      if (!await FormaPagamentoExists(id))
        return NotFound();

      var fornecedor = await ObterFormaPagamentoPorId(id);

      patchDocument.ApplyTo(fornecedor);

      await _formaPagamentoRepository.Atualizar(_mapper.Map<FormaPagamento>(fornecedor));

      return NoContent();
    }

    /// <summary>
    /// Deleta Forma de pagamento
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="204"> Deletado Com Sucesso </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFormaPagamento(Guid id)
    {
      var formaPagamentoApiModel = ObterFormaPagamentoPorId(id);

      if (formaPagamentoApiModel == null) 
        return NotFound();

      await _formaPagamentoRepository.Remover(id);

      return NoContent();
    }

    #region Métodos privados
    private async Task<FormaPagamentoApiModel> ObterFormaPagamentoPorId(Guid id)
    {
      return _mapper.Map<FormaPagamentoApiModel>(await _formaPagamentoRepository.ObterPorId(id));
    }

    private async Task<bool> FormaPagamentoExists(Guid id)
    {
      return _formaPagamentoRepository.Buscar(x => x.Id == id).Result.Any();
    }

    private async Task<IEnumerable<FormaPagamentoApiModel>> ObterAllFormaPgamento(int? pagina, int tamanho, bool ativo)
    {
      var listaFornecedores = await _formaPagamentoRepository.ObterAllFormaPgamento(pagina, tamanho, ativo);
      return _mapper.Map<IEnumerable<FormaPagamentoApiModel>>(listaFornecedores);
    }
    #endregion Métodos privados
  }
}
