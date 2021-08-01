using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IRepository
{
  public interface IFornecedorRepository : IRepository<Fornecedor>
  {
    Task<IList<Fornecedor>> ObterTodosFornecedoresEndereco();
    Task<Fornecedor> ObterFornecedorEndereco(Guid id);
    Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id);
  }
}
