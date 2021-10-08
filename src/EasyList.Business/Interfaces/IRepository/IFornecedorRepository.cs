using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IRepository
{
  public interface IFornecedorRepository : IRepository<Fornecedor>
  {
    Task<IList<Fornecedor>> ObterTodosPorPaginacao(int? pagina, int tamanho = 15, bool ativo = false);
    Task<IList<Fornecedor>> ObterTodosFornecedoresEndereco();
    Task<Fornecedor> ObterFornecedorEndereco(Guid id);
    Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id);
  }
}
