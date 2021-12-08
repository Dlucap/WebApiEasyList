using AutoMapper;
using EasyList.Api.ApiModels;
using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
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

    /// <summary>
    ///  Retorna compras cadastrados no banco
    /// </summary>  
    /// <response code="200"> Sucesso </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CompraApiModel>>> GetCompra()
    {
      var compras = await _compraRepository.ObterTodasCompras();
      var compra = _mapper.Map<IEnumerable<CompraApiModel>>(compras);

      if (compra == null)
        return NotFound();

      return Ok(compra);
    }

    /// <summary>
    ///  Retorna compras cadastrados no banco por Id
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200"> Sucesso </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpGet("{id}")]
    public async Task<ActionResult<CompraApiModel>> GetCompra(Guid id)
    {
      var compra = await ObterCompraPorId(id);

      if (compra == null)
        return NotFound();

      return Ok(compra);
    }

    /// <summary>
    /// Retorna as compras cadastrados no banco
    /// </summary>
    /// <param name="pagina"> Página </param>
    /// <param name="tamanho">Quantidade de registros por página </param>   
    /// <response code="200"> Sucesso </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpGet("{pagina}/{tamanho}/{ativo}")]
    public async Task<ActionResult<CompraApiModel>> GetCompras(int? pagina, int tamanho)
    {
      var compras = await ObterAllCompras(pagina, tamanho);

      if (compras == null)
        return NotFound();

      return Ok(compras);
    }

    /// <summary>
    ///  Retorna o valor total da compra
    /// </summary>
    /// <param name="id"></param>
    /// <response code="200"> Sucesso </response>
    /// <response code="400">Requisição Inválida </response>
    [HttpGet("valor-total-compra/{id}")]
    public async Task<ActionResult<double>> GetValorTotalCompra(Guid id)
    {
      var compra = await CalculaValorTotalCompra(id);

      if (compra == -1)
        return BadRequest();

      return Ok(compra);
    }

    /// <summary>
    /// Insere a Compra
    /// </summary>
    /// <param name="compraApiModel"></param>
    /// <returns></returns>
    /// <response code="201"> Criado com Sucesso </response>
    /// <response code="400"> Requisição Inválida </response>
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

    /// <summary>
    /// Atualiza Compra 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="compraApiModel"></param>
    /// <returns></returns>
    /// <response code="204"> Sucesso </response>
    /// <response code="400"> Requisição Inválida </response>
    /// <response code="500"> Erro Interno do Servidor </response>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCompra(Guid id, CompraApiModel compraApiModel)
    { 
      if (id != compraApiModel.Id)
        return BadRequest();

      if (!await CompraExists(id))
        return NotFound();

      if (!ModelState.IsValid)
        return BadRequest();

      if (!await CompraExists(id))
        return NotFound();

      await _compraRepository.Atualizar(_mapper.Map<Compra>(compraApiModel));

      return NoContent();
    }

    /// <summary>
    /// Atualização Parcial Compra 
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patchDocument"></param>
    /// <returns></returns>
    /// <response code="204"> Atualizado Com Sucesso </response>
    /// <response code="400"> Requisição Inválida </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchCompra(Guid id, JsonPatchDocument<CompraApiModel> patchDocument)
    {
      if (patchDocument == null)
        return BadRequest();

      if (!ModelState.IsValid)
        return BadRequest();

      if (!await CompraExists(id))
        return NotFound();

      var compra = await ObterCompraPorId(id);

      patchDocument.ApplyTo(compra);

      await _compraRepository.Atualizar(_mapper.Map<Compra>(compra));

      return NoContent();
    }

    /// <summary>
    /// Deleta Compra
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="204"> Deletado Com Sucesso </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCompra(Guid id)
    {
      var compraApi = await ObterCompraPorId(id);

      if (compraApi == null)
        return NotFound();

      await _compraRepository.Remover(id);

      return NoContent();
    }

    #region Métodos Privados
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

    private async Task<bool> CompraExists(Guid id)
    {
      return _compraRepository.Buscar(x => x.Id == id).Result.Any();
    }

    private async Task<IEnumerable<CompraApiModel>> ObterAllCompras(int? pagina, int tamanho)
    {
      var listaFornecedores = await _compraRepository.ObterTodosPorPaginacao(pagina, tamanho);
      return _mapper.Map<IEnumerable<CompraApiModel>>(listaFornecedores);
    }
       
    #endregion Métodos Privados
  }
}
