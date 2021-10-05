using AutoMapper;
using EasyList.Api.ApiModels;
using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyList.Api.Controllers
{
  //[Authorize]
  [Route("api/v{version:apiVersion}/[controller]")]
  [ApiController]
  public class ProdutoController : ControllerBase
  {
    private readonly IProdutoRepository _produtoRepository;
    private readonly ICategoriaService _categoriaService;
    private readonly IMapper _mapper;

    public ProdutoController(IProdutoRepository produtoRepository,
                             ICategoriaService categoriaService,
                             IMapper mapper)
    {
      _produtoRepository = produtoRepository;
      _categoriaService = categoriaService;
      _mapper = mapper;
    }
    /// <summary>
    /// Retorna todos produtos cadastrados no banco
    /// </summary>
    /// <response code="200"> Sucesso </response>
    /// <response code="404"> Não Encontrado</response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoApiModel>>> GetProdutos()
    {

      var produtoApiModel = _mapper.Map<IEnumerable<ProdutoApiModel>>(await _produtoRepository.ObterTodos());

      if (produtoApiModel is null)
        return NotFound();

      return Ok(produtoApiModel);
    }

    /// <summary>
    /// Retorna todos produtos cadastrados no banco pelo Id
    /// </summary>
    /// <response code="200"> Sucesso </response>
    /// <response code="404"> Não Encontrado</response>
    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoApiModel>> GetProduto(Guid id)
    {
      var produto = await ObterProdutoPorId(id);

      if (produto == null)
        return NotFound();

      return Ok(produto);
    }

    /// <summary>
    /// Retorna os produtos cadastrados no banco
    /// </summary>
    /// <param name="pagina"> Página </param>
    /// <param name="tamanho">Quantidade de registros por página</param>
    /// <response code="200"> Sucesso </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpGet("{pagina}/{tamanho}", Name = "GetAllProdutos")]
    public async Task<ActionResult<FornecedorApiModel>> GetProdutos(int? pagina, int tamanho)
    {
      var fornecedores = await ObterAllProdutos(pagina, tamanho);

      if (fornecedores == null)
        return NotFound();

      return Ok(fornecedores);
    }

    /// <summary>
    /// Insere o Produto
    /// </summary>
    /// <param name="produtoApiModel"></param>
    /// <returns></returns>
    /// <response code="201"> Criado com Sucesso </response>
    /// <response code="400"> Não Encontrado </response>
    [HttpPost]
    public async Task<ActionResult<ProdutoApiModel>> PostProduto(ProdutoApiModel produtoApiModel)
    {
      if (!ModelState.IsValid)
        BadRequest();

      if (!await _categoriaService.CategoriaExists(produtoApiModel.CategoriaId))
        return BadRequest();

      await _produtoRepository.Adicionar(_mapper.Map<Produto>(produtoApiModel));

      return CreatedAtAction("GetProduto", new { id = produtoApiModel.Id }, produtoApiModel);
    }

    /// <summary>
    /// Atualiza Produto
    /// </summary>
    /// <param name="id"></param>
    /// <param name="produtoApiModel"></param>
    /// <returns></returns>
    /// <response code="200"> Sucesso </response>
    /// <response code="400"> Requisição Inválida </response>
    /// <response code="500"> Erro Interno do Servidor</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduto(Guid id, ProdutoApiModel produtoApiModel)
    {
      if (id != produtoApiModel.Id)
        return BadRequest();

      if (!await _categoriaService.CategoriaExists(produtoApiModel.CategoriaId))
        return BadRequest();

      if (!ModelState.IsValid)
        BadRequest();

      await _produtoRepository.Atualizar(_mapper.Map<Produto>(produtoApiModel));

      return NoContent();
    }

    /// <summary>
    /// Atualização Parcial Produto
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patchDocument"></param>
    /// <returns></returns>
    /// <response code="204"> Sucesso </response>
    /// <response code="400"> Requisição Inválida </response>
    /// <response code="404"> Não Encontrado</response>
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchFornecedor(Guid id, JsonPatchDocument<ProdutoApiModel> patchDocument)
    {
      if (patchDocument == null)
        return BadRequest();

      if (!ModelState.IsValid)
        return BadRequest();

      if (!await ProdutoExists(id))
        return NotFound();

      var produto = await ObterProdutoPorId(id);

      patchDocument.ApplyTo(produto);

      await _produtoRepository.Atualizar(_mapper.Map<Produto>(produto));

      return NoContent();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="204"> Sucesso </response>
    /// <response code="404"> Não Encontrado</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduto(Guid id)
    {
      var produtoApiModel = await ObterProdutoPorId(id);

      if (produtoApiModel == null)
        return NotFound();

      await _produtoRepository.Remover(id);

      return NoContent();
    }

    #region Métodos privados
    private async Task<ProdutoApiModel> ObterProdutoPorId(Guid id)
    {
      return _mapper.Map<ProdutoApiModel>(await _produtoRepository.ObterPorId(id));
    }

    private async Task<IEnumerable<ProdutoApiModel>> ObterAllProdutos(int? pagina, int tamanho)
    {
      var listaProdutos = await _produtoRepository.ObterTodosPorPaginacao(pagina, tamanho);
    
      return _mapper.Map<IEnumerable<ProdutoApiModel>>(listaProdutos);
    }

    private async Task<bool> ProdutoExists(Guid id)
    {
      return _produtoRepository.Buscar(x => x.Id == id).Result.Any();
    }
    #endregion Métodos privados
  }
}
