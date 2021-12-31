using AutoMapper;
using EasyList.Api.ApiModels;
using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using Microsoft.AspNetCore.Authorization;
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
  public class CategoriaController : ControllerBase
  {
    private readonly ICategoriaRepository _categoriaRepository;
    private readonly IMapper _mapper;

    public CategoriaController(ICategoriaRepository categoriaRepository, IMapper mapper)
    {
      _categoriaRepository = categoriaRepository;
      _mapper = mapper;
    }

    // [Authorize]
    /// <summary>
    /// Retorna todas as catefgorias (ativos e inativos) cadastradas no banco
    /// </summary>
    /// <returns> Token de Autenticação</returns>
    /// <response code="200"> Sucesso </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaApiModel>>> GetCategoria()
    {
      var categorias = _mapper.Map<IEnumerable<CategoriaApiModel>>(await _categoriaRepository.ObterTodos());

      if (categorias is null)
        return NotFound();

      return Ok(categorias);
    }

    /// <summary>
    /// Retorna todas as catefgorias (ativos e inativos) cadastradas no banco por Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaApiModel>> GetCategoria(Guid id)
    {
      var categoria = await ObterCategoriaPorId(id);

      if (categoria == null)
        return NotFound();

      return Ok(categoria);
    }

    /// <summary>
    /// Retorna as categorias cadastrados no banco
    /// </summary>
    /// <param name="pagina"> Página </param>
    /// <param name="tamanho">Quantidade de registros por página </param>
    /// <param name="ativo">Ativo 1 / Inativo 0 </param>
    /// <response code="200"> Sucesso </response>
    /// <response code="404"> Não Encontrado </response>
    [HttpGet("{pagina}/{tamanho}/{ativo}")]
    public async Task<ActionResult<FornecedorApiModel>> GetCategorias(int? pagina, int tamanho, bool ativo)
    {
      var categorias = await ObterAllCategorias(pagina, tamanho, ativo);

      if (categorias == null)
        return NotFound();

      return Ok(categorias);
    }

    /// <summary>
    /// Insere a Categoria
    /// </summary>
    /// <param name="categoriaApiModel"></param>
    /// <returns></returns>
    /// <response code="201"> Criado com Sucesso </response>
    /// <response code="400"> Requisição Inválida </response>
    [HttpPost]
    public async Task<ActionResult<CategoriaApiModel>> PostCategoria(CategoriaApiModel categoriaApiModel)
    {
      if (!ModelState.IsValid)
        return BadRequest();

      var categoriaEntity = _mapper.Map<Categoria>(categoriaApiModel);

      await _categoriaRepository.Adicionar(categoriaEntity);

      return CreatedAtAction("GetCategoria", new { id = categoriaApiModel.Id }, categoriaEntity);
    }

    /// <summary>
    /// Atualiza Produto
    /// </summary>
    /// <param name="id"></param>
    /// <param name="categoriaApiModel"></param>
    /// <returns></returns>
    /// <response code="200"> Sucesso </response>
    /// <response code="400"> Requisição Inválida </response>
    /// <response code="500"> Erro Interno do Servidor</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategoria(Guid id, CategoriaApiModel categoriaApiModel)
    {
      if (id != categoriaApiModel.Id)
        return BadRequest();

      if (!ModelState.IsValid)
        return BadRequest();

      if (!await CategoriaExists(id))
        return NotFound();

      await _categoriaRepository.Atualizar(_mapper.Map<Categoria>(categoriaApiModel));

      return NoContent();
    }

    /// <summary>
    /// Atualização Parcial Categoria
    /// </summary>
    /// <param name="id"></param>
    /// <param name="patchDocument"></param>
    /// <returns></returns>
    /// <response code="204"> Sucesso </response>
    /// <response code="400"> Requisição Inválida </response>
    /// <response code="404"> Não Encontrado</response>
    [HttpPatch("{id}")]
    public async Task<IActionResult> PatchFornecedor(Guid id, JsonPatchDocument<CategoriaApiModel> patchDocument)
    {
      if (patchDocument == null)
        return BadRequest();

      if (!ModelState.IsValid)
        return BadRequest();

      if (!await CategoriaExists(id))
        return NotFound();

      var produto = await ObterCategoriaPorId(id);

      patchDocument.ApplyTo(produto);

      await _categoriaRepository.Atualizar(_mapper.Map<Categoria>(produto));

      return NoContent();
    }

    /// <summary>
    /// Deleta Categoria
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <response code="204"> Sucesso </response>
    /// <response code="404"> Não Encontrado</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoria(Guid id)
    {
      var categoriaApiModel = await ObterCategoriaPorId(id);

      if (categoriaApiModel is null)
        return NotFound();

      await _categoriaRepository.Remover(id);

      return NoContent();
    }

    #region Métodos privados
    private async Task<CategoriaApiModel> ObterCategoriaPorId(Guid id)
    {
      return _mapper.Map<CategoriaApiModel>(await _categoriaRepository.ObterPorId(id));
    }

    private async Task<bool> CategoriaExists(Guid id)
    {
      return _categoriaRepository.Buscar(x => x.Id == id).Result.Any();
    }

    private async Task<IEnumerable<CategoriaApiModel>> ObterAllCategorias(int? pagina, int tamanho, bool ativo)
    {
      var listaCategorias = await _categoriaRepository.ObterTodosPorPaginacao(pagina, tamanho, ativo);
      return _mapper.Map<IEnumerable<CategoriaApiModel>>(listaCategorias);
    }


    #endregion Métodos privados

  }
}

