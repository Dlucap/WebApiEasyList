using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyList.Business.Services
{
    public class FormaPagamentoService : IFormaPagamentoService
    {
        public readonly IFormaPagamentoRepository _formaPagamentoRepository;
        public FormaPagamentoService(IFormaPagamentoRepository formaPagamentoRepository)
        {
            _formaPagamentoRepository = formaPagamentoRepository;
        }

        public async Task<bool> Adicionar(FormaPagamento formaPagamento)
        {
            if (_formaPagamentoRepository.Buscar(fp => fp.Id == formaPagamento.Id).Result.Any())
                return false;

            await _formaPagamentoRepository.Adicionar(formaPagamento);
            return true;
        }

        public async Task<bool> Atualizar(FormaPagamento formaPagamento)
        {
            if (!_formaPagamentoRepository.Buscar(fp => fp.Id == formaPagamento.Id).Result.Any())
                return false;

            await _formaPagamentoRepository.Atualizar(formaPagamento);
            return true;
        }

        public async Task<bool> Remover(Guid id)
        {
            if (!_formaPagamentoRepository.Buscar(fp => fp.Id == id).Result.Any())
                return false;

            await _formaPagamentoRepository.Remover(id);
            return true;
        }

        public async Task<FormaPagamento> ObterPorId(Guid id)
        {
            return await _formaPagamentoRepository.ObterPorId(id);
        }

        public async Task<FormaPagamento> ObterFormaPagamentoPorNome(string nomeFormaPagamento)
        {
            return await _formaPagamentoRepository.ObterFormaPagamentoPorNome(nomeFormaPagamento);
        }

        public async Task<IEnumerable<FormaPagamento>> ObterFormasPgamento(int? pagina, int tamanho, bool ativo)
        {
            return await _formaPagamentoRepository.ObterTodosPorPaginacao(pagina, tamanho, ativo);
        }

        public async Task<IEnumerable<FormaPagamento>> ObterTodos()
        {
            return await _formaPagamentoRepository.ObterTodos();
        }

        public async Task<bool> FormaPagamentoExists(Guid id)
        {
            return _formaPagamentoRepository.Buscar(x => x.Id == id).Result.Any();
        }

        public void Dispose()
        {
            _formaPagamentoRepository?.Dispose();
        }       
    }
}
