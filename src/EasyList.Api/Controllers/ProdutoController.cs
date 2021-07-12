﻿using AutoMapper;
using EasyList.Api.ApiModels;
using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProdutoController : ControllerBase
  {
    private readonly IProdutoRepository _produtoRepository;
    private readonly IMapper _mapper;

    public ProdutoController(IProdutoRepository produtoRepository, IMapper mapper)
    {
      _produtoRepository = produtoRepository;
      _mapper = mapper;
    }
       
    [HttpGet]
    public async Task <IEnumerable<ProdutoApiModel>> GetProdutos()
    {
      var produtoApiModel = _mapper.Map<IEnumerable<ProdutoApiModel>>(await _produtoRepository.ObterTodos());

      return produtoApiModel;
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

      if (!ModelState.IsValid) BadRequest();

      try
      {
        await _produtoRepository.Atualizar(_mapper.Map<Produto>(produtoApiModel));
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!ProdutoExists(id))
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
        
    [HttpPost]
    public async Task<ActionResult<ProdutoApiModel>> PostProduto(ProdutoApiModel produtoApiModel)
    {
      if (!ModelState.IsValid) 
        BadRequest();

      await _produtoRepository.Adicionar(_mapper.Map<Produto>(produtoApiModel));

      return CreatedAtAction("GetProduto", new { id = produtoApiModel.Id }, produtoApiModel);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduto(Guid id)
    {
      var produtoApiModel = ObterProdutoPorId(id);

      if (produtoApiModel == null) 
        return NotFound();

      await _produtoRepository.Remover(id);

      return NoContent();
    }

    private bool ProdutoExists(Guid id)
    {
      return _produtoRepository.ProdutoExist(id);
    }

    private async Task<ProdutoApiModel> ObterProdutoPorId(Guid id)
    {
      return _mapper.Map<ProdutoApiModel>(await _produtoRepository.ObterPorId(id));
    }
  }
}
