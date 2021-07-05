using EasyList.Business.Intefaces;
using EasyList.Business.Models;
using System;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces
{
  public interface IProdutoRepository : IRepository<Produto>
  {
    Task<Produto> ObterProdutoPorId(Guid id);

    Task<Produto> ObterProdutoPorNome(string nome);

    bool ProdutoExist(Guid id);
  }
}
