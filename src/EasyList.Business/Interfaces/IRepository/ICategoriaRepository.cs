using EasyList.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IRepository
{
  public interface ICategoriaRepository : IRepository<Categoria>
  {
    Task<IEnumerable<Categoria>> ObterTodosPorPaginacao(int? pagina, int tamanho = 15, bool ativo = false);
  }
}
