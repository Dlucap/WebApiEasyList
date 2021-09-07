using AutoMapper;
using EasyList.Api.ApiModels;
using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using X.PagedList;

namespace EasyList.Api.Controllers
{
  [Route("api/[controller]")]
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

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoApiModel>>> GetProdutos( )
    {   

      var produtoApiModel = _mapper.Map<IEnumerable<ProdutoApiModel>>(await _produtoRepository.ObterTodos());

      if (produtoApiModel is null)
        return NotFound();

      return Ok(produtoApiModel);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProdutoApiModel>> GetProduto(Guid id)
    {
      var produto = await ObterProdutoPorId(id);

      if (produto == null)
        return NotFound();

      return Ok(produto);
    }

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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduto(Guid id)
    {
      var produtoApiModel = await ObterProdutoPorId(id);

      if (produtoApiModel == null)
        return NotFound();

      await _produtoRepository.Remover(id);

      return NoContent();
    }

    private async Task<ProdutoApiModel> ObterProdutoPorId(Guid id)
    {
      return _mapper.Map<ProdutoApiModel>(await _produtoRepository.ObterPorId(id));
    }
  }
}
