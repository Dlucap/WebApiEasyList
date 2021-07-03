﻿using AutoMapper;
using EasyList.Api.ApiModels;
using EasyList.Business.Interfaces;
using EasyList.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class FornecedorController : ControllerBase
  {
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly IEnderecoRepository _enderecoRepository;
    private readonly IFornecedorService _fornecedorService;
    private readonly IMapper _mapper;

    public FornecedorController(IFornecedorRepository fornecedorRepository,
                                IEnderecoRepository enderecoRepository,
                                IFornecedorService fornecedorService,
                                IMapper mapper)

    {
      _fornecedorRepository = fornecedorRepository;
      _enderecoRepository = enderecoRepository;
      _fornecedorService = fornecedorService;
      _mapper = mapper;
    }

    // GET: api/Fornecedor
    [HttpGet]
    public async Task<IEnumerable<FornecedorApiModel>> GetFornecedor()
    {
      var fornecedores = _mapper.Map<IEnumerable<FornecedorApiModel>>(await _fornecedorRepository.ObterTodos());

      return fornecedores;
    }

    // GET: api/Fornecedor/5
    [HttpGet("{id}")]
    public async Task<ActionResult<FornecedorApiModel>> GetFornecedor(int id)
    {
      var fornecedor = await _fornecedorRepository.ObterFornecedorPorEndereco(id);

      if (fornecedor == null) return NotFound();
     
      return Ok(fornecedor);
    }

    // PUT: api/Fornecedor/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutFornecedor(int id, FornecedorApiModel fornecedorApiModel)
    {
      if (id != fornecedorApiModel.Id)  return BadRequest();
    
      if (!ModelState.IsValid) BadRequest();

      await _fornecedorRepository.Atualizar(_mapper.Map<Fornecedor>(fornecedorApiModel));
   
      return NoContent();
    }

    // POST: api/Fornecedor
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<FornecedorApiModel>> PostFornecedor(FornecedorApiModel fornecedorApiModel)
    {
      if (!ModelState.IsValid) BadRequest();

      await _fornecedorRepository.Adicionar(_mapper.Map<Fornecedor>(fornecedorApiModel));

      return CreatedAtAction("GetFornecedor", new { id = fornecedorApiModel.Id }, fornecedorApiModel);
    }

    // DELETE: api/Fornecedor/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFornecedor(int id)
    {
      var fornecedorApiModel = await ObterFornecedorPorEndereco(id);
      {
        if (fornecedorApiModel == null) return NotFound();

        await _fornecedorService.Remover(id);

        return NoContent();
      }
    }

    [HttpGet("obter-endereco/{id:guid}")]
    public async Task<EnderecoApiModel> ObterEnderecoPorId(int id)
    {
      var enderecoViewModel = _mapper.Map<EnderecoApiModel>(await _enderecoRepository.ObterPorId(id));
      return enderecoViewModel;
    }

    [HttpPut("atualizar-endereco/{id:guid}")]
    public async Task<IActionResult> AtualizarEndereco(int id, EnderecoApiModel enderecoApiModel)
    {
      if (id !=enderecoApiModel.Id) return BadRequest();

      await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(enderecoApiModel));

      return NoContent();
    }

    private async Task<FornecedorApiModel> ObterFornecedorPorEndereco(int id)
    {
      return _mapper.Map<FornecedorApiModel>(await _fornecedorRepository.ObterFornecedorPorEndereco(id));
    }
   
  }
}