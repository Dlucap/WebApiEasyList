using EasyList.Business.Models;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IRepository
{
    public interface IProdutoRepository : IRepository<Produto>
    {

        Task<Produto> ObterProdutoPorNome(string nome);
        Task<bool> ProdutoExiste(string nome);

    }
}
