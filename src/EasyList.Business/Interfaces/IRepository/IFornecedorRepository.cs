using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IRepository
{
  public interface IFornecedorRepository : IRepository<Fornecedor>
  {
    Task<IEnumerable<Fornecedor>> ObterTodosPorPaginacao(int? pagina, int tamanho = 15, bool ativo = false);
    Task<IEnumerable<Fornecedor>> ObterTodosFornecedoresEndereco();   
    Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id);
    Task<Fornecedor> ObterFornecedorEndereco(Guid id);
  }
}
