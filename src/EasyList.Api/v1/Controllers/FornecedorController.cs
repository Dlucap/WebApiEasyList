using AutoMapper;
using EasyList.Api.ApiModels;
using EasyList.Api.v1.Controllers;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Api.V1.Controllers
{
#if !DEBUG
  [Authorize]
#endif
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FornecedorController : ApiControllerBase
    {
        private readonly IEnderecoService _enderecoService;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedorController(IEnderecoService enderecoService,
                                    IFornecedorService fornecedorService,
                                    IMapper mapper)

        {
            _enderecoService = enderecoService;
            _fornecedorService = fornecedorService;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todos os fornecedores (ativos e inativos) cadastrados no banco
        /// </summary>
        /// <response code="200"> Sucesso </response>
        /// <response code="404"> Não Encontrado </response>
        [HttpGet("{pagina}/{tamanho}/{ativo}")]
        public async Task<ActionResult<IEnumerable<FornecedorApiModel>>> GetFornecedor(int? pagina, int tamanho, bool ativo)
        {
            var fornecedoresModel = await ObterAllFornecedores(pagina, tamanho, ativo);

            if (fornecedoresModel is null)
                return NotFound();

            var fornecedoresApiModel = _mapper.Map<IEnumerable<FornecedorApiModel>>(fornecedoresModel);

            return Ok(fornecedoresApiModel);
        }

        /// <summary>
        ///  Retorna fornecedor (ativos e inativos) cadastrados no banco por Id
        /// </summary>
        /// <param name="id"></param>
        /// <response code="200"> Sucesso </response>
        /// <response code="404"> Não Encontrado </response>
        [HttpGet("{id:guid}", Name = "GetFornecedor")]
        public async Task<ActionResult<FornecedorApiModel>> GetFornecedor(Guid id)
        {
            var fornecedor = await ObterFornecedorPorId(id);

            if (fornecedor == null)
                return NotFound();

            return Ok(fornecedor);
        }
              
        /// <summary>
        ///  Obtém endereço Fornecedor por Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200"> Sucesso </response>
        /// <response code="404"> Não Encontrado </response>
        [HttpGet("obter-endereco-fornecedor/{id:guid}")]
        public async Task<ActionResult<EnderecoApiModel>> ObterEnderecoFornecedorPorId(Guid id)
        {
            if (Guid.Empty == id)                
                return BadRequest();

            var endereco = await _enderecoService.ObterEnderecoPorId(id);

            if (endereco == null)
                return NotFound();

            var enderecoViewModel = _mapper.Map<EnderecoApiModel>(endereco);

            return Ok(enderecoViewModel);
        }

        /// <summary>
        /// Insere o Fornecedor
        /// </summary>
        /// <param name="fornecedorApiModel"></param>
        /// <returns></returns>
        /// <response code="201"> Criado com Sucesso </response>
        /// <response code="400"> Requisição Inválida </response>
        [HttpPost]
        public async Task<ActionResult<FornecedorApiModel>> PostFornecedor(FornecedorApiModel fornecedorApiModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            fornecedorApiModel.Endereco.UsuarioCriacao = ObterUsuarioSessao().UserName;

            var fornecedorEntity = _mapper.Map<Fornecedor>(fornecedorApiModel);

            await _fornecedorService.Adicionar(fornecedorEntity);

            fornecedorApiModel.Id = fornecedorApiModel.Endereco.FornecedorId = fornecedorEntity.Id;
            fornecedorApiModel.Endereco.Id = fornecedorEntity.Endereco.Id;

            return CreatedAtAction("GetFornecedor", new { id = fornecedorApiModel.Id }, fornecedorApiModel);
        }

        /// <summary>
        /// Atualiza Fornecedor 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="fornecedorApiModel"></param>
        /// <returns></returns>
        /// <response code="204"> Sucesso </response>
        /// <response code="400"> Requisição Inválida </response>
        /// <response code="500"> Erro Interno do Servidor </response>
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> PutFornecedor(Guid id, FornecedorApiModel fornecedorApiModel)
        {
            if (id != fornecedorApiModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!await FornecedorExists(id))
                return NotFound();


            fornecedorApiModel.UsuarioModificacao = ObterUsuarioSessao().UserName;
            fornecedorApiModel.Endereco.UsuarioModificacao = ObterUsuarioSessao().UserName;

            await _fornecedorService.Atualizar(_mapper.Map<Fornecedor>(fornecedorApiModel));

            return NoContent();
        }

        /// <summary>
        ///  Atualiza Endereço Fornecedor 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="enderecoApiModel"></param>
        /// <returns></returns>
        /// <response code="204"> Atualizado Com Sucesso </response>
        /// <response code="400"> Requisição Inválida </response>
        /// <response code="500"> Erro Interno do Servidor </response>
        [HttpPut("atualizar-endereco/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id, EnderecoApiModel enderecoApiModel)
        {
            if (id != enderecoApiModel.Id)
                return BadRequest();

            enderecoApiModel.UsuarioModificacao = ObterUsuarioSessao().UserName;

            await _fornecedorService.AtualizarEndereco(_mapper.Map<Endereco>(enderecoApiModel));

            return NoContent();
        }

        /// <summary>
        /// Atualização Parcial Fornecedor
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        /// <response code="204"> Atualizado Com Sucesso </response>
        /// <response code="400"> Requisição Inválida </response>
        /// <response code="404"> Não Encontrado </response>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchFornecedor(Guid id, JsonPatchDocument<FornecedorApiModel> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!await FornecedorExists(id))
                return NotFound();

            var fornecedor = await ObterFornecedorPorId(id);

            patchDocument.ApplyTo(fornecedor);
            // todo: identificar qual entidade esta sendo atualizada para adicionar o usuarioModificação

            await _fornecedorService.Atualizar(_mapper.Map<Fornecedor>(fornecedor));

            return NoContent();
        }

        /// <summary>
        /// Deleta Forncedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204"> Deletado Com Sucesso </response>
        /// <response code="404"> Não Encontrado </response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFornecedor(Guid id)
        {
            var fornecedorApiModel = await ObterFornecedorPorId(id);

            if (fornecedorApiModel == null)
                return NotFound();

            await _fornecedorService.Remover(id);

            return NoContent();
        }

        #region Metodos Privados
        private async Task<FornecedorApiModel> ObterFornecedorPorId(Guid id)
        {
            return _mapper.Map<FornecedorApiModel>(await _fornecedorService.ObterFornecedorEndereco(id));
        }

        private async Task<IEnumerable<FornecedorApiModel>> ObterAllFornecedores(int? pagina, int tamanho, bool ativo)
        {
            var listaFornecedores = await _fornecedorService.ObterTodosPorPaginacao(pagina, tamanho, ativo);
            return _mapper.Map<IEnumerable<FornecedorApiModel>>(listaFornecedores);
        }

        private async Task<bool> FornecedorExists(Guid id)
        {
            return await _fornecedorService.FornecedorExists(id);
        }
        #endregion Metodos Privados
    }
}
