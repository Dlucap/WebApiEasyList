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
using Microsoft.AspNetCore.JsonPatch;

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

    /// <summary>
    /// Retorna todos os fornecedores (ativos e inativos) cadastrados no banco para o usuário e compra específica
    /// </summary>
    /// <param name="idCompra"> Id da compra </param>
    /// <param name="user"> Usuário </param>
    /// <returns></returns>    
    [HttpGet("{id}")]
    public async Task<ActionResult<ItmCompraApiModel>> GetItmCompra(Guid idCompra, string user)
    {//todo: Tratativa quando a compra for compartilhda
      var itmCompra = await ObterTodosItensCompra(idCompra, user);

      if (!itmCompra.Any())
        return NotFound();

      return Ok(itmCompra);
    }

    /// <summary>
    /// Retorna os fornecedores cadastrados no banco
    /// </summary>
    /// <param name="pagina"> Página </param>
    /// <param name="tamanho">Quantidade de registros por página </param>
    /// <param name="ativo">Ativo 1 / Inativo 0 </param>
    /// <response code="200"> Sucesso </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpGet("{pagina}/{tamanho}/{ativo}")]
    public async Task<ActionResult<ItmCompraApiModel>> GetItmCompra(int? pagina, int tamanho, bool ativo)
    {
      var fornecedores = await ObterAllFornecedores(pagina, tamanho, ativo);

      if (fornecedores == null)
        return NotFound();

      return Ok(fornecedores);
    }

    /// <summary>
    /// Insere o Itens de compra
    /// </summary>
    /// <param name="itemCompraApiModel"></param>
    /// <returns></returns>
    /// <response code="201"> Criado com Sucesso </response>
    /// <response code="400"> Requisição Inválida </response>
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

    /// <summary>
    /// Atualiza Item Compra 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="compraId"></param>
    /// <param name="itemCompraApiModel"></param>
    /// <returns></returns>
    /// <response code="204"> Sucesso </response>
    /// <response code="400"> Requisição Inválida </response>
    /// <response code="500"> Erro Interno do Servidor </response>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutItmCompra(Guid id, Guid compraId, ItmCompraApiModel itemCompraApiModel)
    {
      if (id != itemCompraApiModel.Id || compraId != itemCompraApiModel.CompraId)
        return BadRequest();

      if (!ModelState.IsValid)
        return BadRequest();

      if (!await ItmCompraExists(id, compraId))
        return NotFound();

      await _itmCompraService.Atualizar(_mapper.Map<ItmCompra>(itemCompraApiModel));

      return NoContent();
    }

    /// <summary>
    /// Atualização Parcial Item compra
    /// </summary>
    /// <param name="id"></param>
    /// <param name="compraId"></param>
    /// <param name="patchDocument"></param>
    /// <returns></returns>
    /// <response code="204"> Atualizado Com Sucesso </response>
    /// <response code="400"> Requisição Inválida </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchItemCompra(Guid id, Guid compraId, JsonPatchDocument<ItmCompraApiModel> patchDocument)
    {
      if (patchDocument == null)
        return BadRequest();

      if (!ModelState.IsValid)
        return BadRequest();

      if (!await ItmCompraExists(id,compraId))
        return NotFound();

      var itmCompra = await ObterItemCompraPorId(id, compraId);

      patchDocument.ApplyTo(itmCompra);

      await _itmCompraService.Atualizar(_mapper.Map<ItmCompra>(itmCompra));

      return NoContent();
    }

    /// <summary>
    /// Deleta Item Compra
    /// </summary>
    /// <param name="id"> Id do Item da Compra </param>
    /// <param name="compraId"> Id da Compra </param>
    /// <returns></returns>
    /// <response code="204"> Deletado Com Sucesso </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteItmCompra(Guid id, Guid compraId)
    {
      var itemCompra = await ObterItemCompraPorId(id,compraId);

      if (itemCompra == null)
        return NotFound();

      await _itmCompraService.Remover(id);

      return NoContent();
    }

    #region Métodos privados
    private async Task<ItmCompraApiModel> ObterItemCompraPorId(Guid id, Guid compraId)
    {
      return _mapper.Map<ItmCompraApiModel>(await _itmCompraService.ObterPorId(id, compraId));
    }

    private async Task<IEnumerable<ItmCompraApiModel>> ObterTodosItensCompra(Guid idCompra, string userName)
    {
      var listaItensDaCompra = await _itmCompraService.BuscarItensCompra(idCompra, userName);

      return _mapper.Map<IEnumerable<ItmCompraApiModel>>(listaItensDaCompra);
    }

    private async Task<bool> ItmCompraExists(Guid id, Guid compraId)
    {
      return _itmCompraRepository.Buscar(x => x.Id == id && x.CompraId == compraId).Result.Any();
    }

    private async Task<IEnumerable<ItmCompraApiModel>> ObterAllFornecedores(int? pagina, int tamanho, bool ativo)
    {
      var listaFornecedores = await _itmCompraRepository.ObterTodosPorPaginacao(pagina, tamanho);
      return _mapper.Map<IEnumerable<ItmCompraApiModel>>(listaFornecedores);
    }

    #endregion Métodos privados
  }
}
