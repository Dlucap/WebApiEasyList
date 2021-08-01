using AutoMapper;
using EasyList.Api.ApiModels;
using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using Microsoft.AspNetCore.Mvc;
using System;
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

    [HttpGet]
    public async Task<IEnumerable<FornecedorApiModel>> GetFornecedor()
    {
      var fornecedoresModel = _mapper.Map<IEnumerable<FornecedorApiModel>>(await ObterFornecedoresEndereco());

      return fornecedoresModel;
    }

    [HttpGet("{id}", Name = "GetFornecedor")]
    public async Task<ActionResult<FornecedorApiModel>> GetFornecedor(Guid id)
    {
      var fornecedor = await ObterFornecedorPorId(id);

      if (fornecedor == null)
        return NotFound();

      return Ok(fornecedor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutFornecedor(Guid id, FornecedorApiModel fornecedorApiModel)
    {
      if (id != fornecedorApiModel.Id)
        return BadRequest();

      if (!ModelState.IsValid)
        return BadRequest();

      await _fornecedorRepository.Atualizar(_mapper.Map<Fornecedor>(fornecedorApiModel));

      return NoContent();
    }

    [HttpPost]
    public async Task<ActionResult<FornecedorApiModel>> PostFornecedor(FornecedorApiModel fornecedorApiModel)
    {
      if (!ModelState.IsValid)
        return BadRequest();

      var fornecedorEntity = _mapper.Map<Fornecedor>(fornecedorApiModel);

      await _fornecedorRepository.Adicionar(fornecedorEntity);

      fornecedorApiModel.Id = fornecedorApiModel.Endereco.FornecedorId = fornecedorEntity.Id;
      fornecedorApiModel.Endereco.Id = fornecedorEntity.Endereco.Id;

      return CreatedAtAction("GetFornecedor", new { id = fornecedorApiModel.Id }, fornecedorApiModel);

    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFornecedor(Guid id)
    {
      var fornecedorApiModel = await ObterFornecedorPorId(id);

      if (fornecedorApiModel == null)
        return NotFound();

      await _fornecedorService.Remover(id);

      return NoContent();

    }

    [HttpGet("obter-endereco/{id:guid}")]
    public async Task<EnderecoApiModel> ObterEnderecoPorId(Guid id)
    {
      var enderecoViewModel = _mapper.Map<EnderecoApiModel>(await _enderecoRepository.ObterPorId(id));
      return enderecoViewModel;
    }

    [HttpPut("atualizar-endereco/{id:guid}")]
    public async Task<IActionResult> AtualizarEndereco(Guid id, EnderecoApiModel enderecoApiModel)
    {
      if (id != enderecoApiModel.Id)
        return BadRequest();

      await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(enderecoApiModel));

      return NoContent();
    }

    private async Task<FornecedorApiModel> ObterFornecedorPorId(Guid id)
    {
      return _mapper.Map<FornecedorApiModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
    }

    private async Task<IEnumerable<FornecedorApiModel>> ObterFornecedoresEndereco()
    {
      var listaFornecedores = await _fornecedorRepository.ObterTodosFornecedoresEndereco();
      return _mapper.Map<IEnumerable<FornecedorApiModel>>(listaFornecedores);
    }

  }
}
