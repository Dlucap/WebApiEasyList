using AutoMapper;
using EasyList.Api.ApiModels;
using EasyList.Api.v1.Controllers;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FormaPagamentoController : ApiControllerBase
    {
        private readonly IFormaPagamentoService _formaPagamentoServices;
        private readonly IMapper _mapper;

        public FormaPagamentoController(IFormaPagamentoService formaPagamentoServices,
                                        IMapper mapper)
        {
            _formaPagamentoServices = formaPagamentoServices;
            _mapper = mapper;
        }

        /// <summary>
        /// Retorna todas as formas de pagamento cadastradas ativas e não ativas.
        /// </summary>
        /// <returns> retorna todas as formas de pagamento cadastradas ativas e não ativas.</returns>
        /// <response code="200"> sucesso maluco </response>
        /// <response code="404"> Não Encontrado </response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FormaPagamentoApiModel>>> GetFormaPagamento()
        {
            var formaPagamento = await _formaPagamentoServices.ObterTodos();
           
            if (formaPagamento is null)
                return NotFound();

            var formaPagamentoApiModel = _mapper.Map<IEnumerable<FormaPagamentoApiModel>>(formaPagamento);

            return Ok(formaPagamentoApiModel);
        }

        /// <summary>
        /// Retorna todas as formas de pagamento cadastradas ativas e não ativas por id
        /// </summary>
        /// <returns> retorna todas as formas de pagamento cadastradas ativas e não ativas.</returns>
        /// <response code="200"> sucesso maluco </response>
        /// <response code="404"> Não Encontrado </response>
        [HttpGet("{id}")]
        public async Task<ActionResult<FormaPagamentoApiModel>> GetFormaPagamento(Guid id)
        {
            var prodformaPagamentoApiModeluto = await ObterFormaPagamentoPorId(id);

            if (prodformaPagamentoApiModeluto == null)
                return NotFound();

            return Ok(prodformaPagamentoApiModeluto);
        }

        /// <summary>
        /// Retorna as formas de pagamento cadastrados no banco
        /// </summary>
        /// <param name="pagina"> Página </param>
        /// <param name="tamanho"> Quantidade de registros por página </param>
        /// <param name="ativo">Ativo 1 / Inativo 0 </param>
        /// <response code="200"> Sucesso </response>
        /// <response code="404"> Não Encontrado </response>
        [HttpGet("{pagina}/{tamanho}/{ativo}")]
        public async Task<ActionResult<FornecedorApiModel>> GetFormaPagamento(int? pagina, int tamanho, bool ativo)
        {
            var fornecedores = await ObterAllFormaPgamento(pagina, tamanho, ativo);

            if (fornecedores == null)
                return NotFound();

            return Ok(fornecedores);
        }

        /// <summary>
        /// Insere o Forma de pagamento
        /// </summary>
        /// <param name="formaPagamentoApiModel"></param>
        /// <returns></returns>
        /// <response code="201"> Criado com Sucesso </response>
        /// <response code="400"> Requisição Inválida </response>
        [HttpPost]
        public async Task<ActionResult<FormaPagamentoApiModel>> PostFormaPagamento(FormaPagamentoApiModel formaPagamentoApiModel)
        {
            if (!ModelState.IsValid)
                BadRequest();

            formaPagamentoApiModel.UsuarioCriacao = ObterUsuarioSessao().UserName;

            var formaPagamentoEntity = _mapper.Map<FormaPagamento>(formaPagamentoApiModel);

            await _formaPagamentoServices.Adicionar(formaPagamentoEntity);

            formaPagamentoApiModel.Id = formaPagamentoEntity.Id;

            return CreatedAtAction("GetFormaPagamento", new { id = formaPagamentoApiModel.Id }, formaPagamentoApiModel);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="formaPagamentoApiModel"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFormaPagamento(Guid id, FormaPagamentoApiModel formaPagamentoApiModel)
        {
            if (id != formaPagamentoApiModel.Id)
                return BadRequest();

            if (!ModelState.IsValid)
                BadRequest();

            if (!await FormaPagamentoExists(id))
                return NotFound();

            formaPagamentoApiModel.UsuarioModificacao = ObterUsuarioSessao().UserName;

            await _formaPagamentoServices.Atualizar(_mapper.Map<FormaPagamento>(formaPagamentoApiModel));

            return NoContent();
        }

        /// <summary>
        /// Atualização Parcial FormaPagamento
        /// </summary>
        /// <param name="id"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        /// <response code="204"> Atualizado Com Sucesso </response>
        /// <response code="400"> Requisição Inválida </response>
        /// <response code="404"> Não Encontrado </response>
        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchFormaPagamento(Guid id, JsonPatchDocument<FormaPagamentoApiModel> patchDocument)
        {
            if (patchDocument == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();

            if (!await FormaPagamentoExists(id))
                return NotFound();

            var formaPagamento = await ObterFormaPagamentoPorId(id);

            patchDocument.ApplyTo(formaPagamento);
          
            formaPagamento.UsuarioModificacao = ObterUsuarioSessao().UserName;

            await _formaPagamentoServices.Atualizar(_mapper.Map<FormaPagamento>(formaPagamento));

            return NoContent();
        }

        /// <summary>
        /// Deleta Forma de pagamento
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="204"> Deletado Com Sucesso </response>
        /// <response code="404"> Não Encontrado </response>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFormaPagamento(Guid id)
        {
            var formaPagamentoApiModel = ObterFormaPagamentoPorId(id);

            if (formaPagamentoApiModel == null)
                return NotFound();

            await _formaPagamentoServices.Remover(id);

            return NoContent();
        }

        #region Métodos privados
        private async Task<FormaPagamentoApiModel> ObterFormaPagamentoPorId(Guid id)
        {
            return _mapper.Map<FormaPagamentoApiModel>(await _formaPagamentoServices.ObterPorId(id));
        }

        private async Task<bool> FormaPagamentoExists(Guid id)
        {
            return await _formaPagamentoServices.FormaPagamentoExists(id);
        }

        private async Task<IEnumerable<FormaPagamentoApiModel>> ObterAllFormaPgamento(int? pagina, int tamanho, bool ativo)
        {
            var listaFornecedores = await _formaPagamentoServices.ObterFormasPgamento(pagina, tamanho, ativo);
            return _mapper.Map<IEnumerable<FormaPagamentoApiModel>>(listaFornecedores);
        }
        #endregion Métodos privados
    }
}
