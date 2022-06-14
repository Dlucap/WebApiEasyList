using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IServices
{
    public interface IFormaPagamentoService : IDisposable
    {
        Task<bool> Adicionar(FormaPagamento formaPagamento);
        Task<bool> Atualizar(FormaPagamento formaPagamento);
        Task<bool> Remover(Guid id);

        Task<FormaPagamento> ObterPorId(Guid id);
        Task<FormaPagamento> ObterFormaPagamentoPorNome(string nomeFormaPagamento);
        Task<IEnumerable<FormaPagamento>> ObterFormasPgamento(int? pagina, int tamanho, bool ativo);
        Task<IEnumerable<FormaPagamento>> ObterTodos();
        Task<bool> FormaPagamentoExists(Guid id);

    }
}
