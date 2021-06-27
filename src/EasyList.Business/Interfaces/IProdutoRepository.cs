using EasyList.Business.Intefaces;
using EasyList.Business.Models;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces
{
  public interface IProdutoRepository : IRepository<Produto>
  {
    Task<Produto> ObterProdutoPorId(int id);

    Task<Produto> ObterProdutoPorNome(string nome);

    bool ProdutoExist(int id);
  }
}
