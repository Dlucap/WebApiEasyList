using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IServices
{
  public interface ICategoriaService : IDisposable
  {
    Task<bool> Adicionar(Categoria categoria);
    Task<bool> Atualizar(Categoria categoria);
    Task<bool> Remover(Guid id);

    Task<bool> CategoriaExists(Guid Id);
    Task<Categoria> ObterPorId(Guid id);
    Task<IEnumerable<Categoria>> ObterTodosPorPaginacao(int? pagina, int tamanho = 15, bool ativo = false);
    Task<IEnumerable<Categoria>> ObterTodasCategorias();
  }
}
