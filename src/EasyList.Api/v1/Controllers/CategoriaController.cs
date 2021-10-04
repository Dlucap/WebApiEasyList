using AutoMapper;
using EasyList.Api.ApiModels;
using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Api.Controllers
{
  //[Authorize]
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
    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoriaApiModel>>> GetCategoria()
    {
      var categorias = _mapper.Map<IEnumerable<CategoriaApiModel>>(await _categoriaRepository.ObterTodos());

      if (categorias is null)
        return NotFound();

      return Ok(categorias);
    }
        
    [HttpGet("{id}")]
    public async Task<ActionResult<CategoriaApiModel>> GetCategoria(Guid id)
    {
      var categoria = await ObterCategoriaPorId(id);

      if (categoria == null)
        return NotFound();

      return Ok(categoria);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategoria(Guid id, CategoriaApiModel categoriaApiModel)
    {
      if (id != categoriaApiModel.Id)
        return BadRequest();

      if (!ModelState.IsValid)
        return BadRequest();

      await _categoriaRepository.Atualizar(_mapper.Map<Categoria>(categoriaApiModel));

      return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<CategoriaApiModel>> PostCategoria(CategoriaApiModel categoriaApiModel)
    {
      if (!ModelState.IsValid)
        return BadRequest();

      await _categoriaRepository.Atualizar(_mapper.Map<Categoria>(categoriaApiModel));

      return CreatedAtAction("GetCategoria", new { id = categoriaApiModel.Id }, categoriaApiModel);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoria(Guid id)
    {
      var categoriaApiModel = await ObterCategoriaPorId(id);

      if (categoriaApiModel is null)
        return NotFound();

      await _categoriaRepository.Remover(id);

      return NoContent();
    }

    private async Task<CategoriaApiModel> ObterCategoriaPorId(Guid id)
    {
      return _mapper.Map<CategoriaApiModel>(await _categoriaRepository.ObterPorId(id));
    }

  }
}

