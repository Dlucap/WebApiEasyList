using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IServices
{
    public interface IProdutoService : IDisposable
    {
        Task<bool> Adicionar(Produto produto);
        Task<bool> Atualizar(Produto produto);
        Task<bool> Remover(Guid id);

        Task<IEnumerable<Produto>> ObterTodos();
        Task<Produto> ObterPorId(Guid Id);
        Task<IEnumerable<Produto>> ObterTodosPorPaginacao(int? pagina, int tamanho);
        Task<bool> Buscar(Guid id);
    }
}
