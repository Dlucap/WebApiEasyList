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

namespace EasyList.Api.V1.Controllers
{
#if !DEBUG
  [Authorize]
#endif
  [ApiVersion("1.0")]
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

    /// <summary>
    ///  Retorna as compras compartilhadas (ativos e inativos) cadastrados no banco
    /// </summary>
    /// <response code="200"> Sucesso </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompraCompartilhadaApiModel>>> GetCompraCompartilhada()
    {
      var compraCompartilhada = _mapper.Map<IEnumerable<CompraCompartilhadaApiModel>>(await _CompraCompartilhadaRepository.ObterTodos());

      if (compraCompartilhada is null)
        return NotFound();

      return Ok(compraCompartilhada);
    }

    /// <summary>
    ///  Retorna as compras compartilhadas (ativos e inativos) cadastrados no banco por Id
    /// </summary>
    /// <param name="idCompra"></param>
    /// <response code="200"> Sucesso </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpGet("{idCompra}")]
    public async Task<ActionResult<CompraCompartilhadaApiModel>> GetCompraCompartilhada(Guid idCompra)
    {
      var compraCompartilhada = await ObterCompraCompartilhadaPorId(idCompra);

      if (compraCompartilhada == null)
        return NotFound();

      return Ok(compraCompartilhada);
    }

    /// <summary>
    /// Insere o Compartilhamento da compra
    /// </summary>
    /// <param name="compraCompartilhadaApiModel"></param>
    /// <returns></returns>
    /// <response code="201"> Criado com Sucesso </response>
    /// <response code="400"> Requisição Inválida </response>
    [HttpPost]
    public async Task<ActionResult<Compra>> PostCompraCompartilhada(CompraCompartilhadaApiModel compraCompartilhadaApiModel)
    {
      if (!ModelState.IsValid)
        return BadRequest();

      var compraCOmpartilhadaEntity = _mapper.Map<CompraCompartilhada>(compraCompartilhadaApiModel);

      await _CompraCompartilhadaRepository.Adicionar(compraCOmpartilhadaEntity);

      return CreatedAtAction("GetCompraCompartilhada", new { id = compraCompartilhadaApiModel.Id }, compraCOmpartilhadaEntity);
    }

    /// <summary>
    /// Atualiza Compra  Compartilhada 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="compraCompartilhadaApiModel"></param>
    /// <returns></returns>
    /// <response code="204"> Sucesso </response>
    /// <response code="400"> Requisição Inválida </response>
    /// <response code="500"> Erro Interno do Servidor </response>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompraCompartilhada(Guid id, CompraCompartilhadaApiModel compraCompartilhadaApiModel)
    {
      if (id != compraCompartilhadaApiModel.Id)
        return BadRequest();

      if (!ModelState.IsValid)
        return BadRequest();

      if (!await CompraCompartilhadaExists(compraCompartilhadaApiModel.CompraId))
        return NotFound();

      await _CompraCompartilhadaRepository.Atualizar(_mapper.Map<CompraCompartilhada>(compraCompartilhadaApiModel));

      return NoContent();
    }

    /// <summary>
    /// Atualização Parcial Compra Compartilhada
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patchDocument"></param>
    /// <returns></returns>
    /// <response code="204"> Atualizado Com Sucesso </response>
    /// <response code="400"> Requisição Inválida </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchCompraCompartilhada(Guid id, JsonPatchDocument<CompraCompartilhadaApiModel> patchDocument)
    {
      if (patchDocument == null)
        return BadRequest();

      if (!ModelState.IsValid)
        return BadRequest();

      var compraCompartilhada = await ObterCompraCompartilhadaPorId(id);

      patchDocument.ApplyTo(compraCompartilhada);

      await _CompraCompartilhadaRepository.Atualizar(_mapper.Map<CompraCompartilhada>(compraCompartilhada));

      return NoContent();
    }

    /// <summary>
    /// Retirar o compartilhamentod compra com o usuário
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="204"> Deletado Com Sucesso </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompartilhamentoCompra(Guid id)
    {
      var compraCompartilhadaApiModel = await ObterCompraCompartilhadaPorId(id);

      if (compraCompartilhadaApiModel == null)
        return NotFound();

      await _CompraCompartilhadaRepository.Remover(id);

      return NoContent();
    }

    #region Métodos privados
    private async Task<CompraCompartilhadaApiModel> ObterCompraCompartilhadaPorId(Guid id)
    {
      var compraCompartilhada = _mapper.Map<CompraCompartilhadaApiModel>(await _CompraCompartilhadaRepository.ObterPorId(id));

      return compraCompartilhada;
    }

    /// <summary>
    /// Verifica se compra está compartilhada
    /// </summary>
    /// <param name="id">Id. compra compartilhada</param>
    /// <returns></returns>
    private async Task<bool> CompraCompartilhadaExists(Guid id)
    {
      return _CompraCompartilhadaRepository.Buscar(x => x.CompraId == id).Result.Any();
    }
    #endregion Métodos privados
  }
}
//todo: estudar como sera feito o descompartilhamento da compra com o usuário, será filtrado por nome do usuário ou codigo