using EasyList.Business.Intefaces;
using EasyList.Business.Models;
using System;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces
{
  public interface ICategoriaRepository : IRepository<Categoria>
  {
     Task<Categoria> ObterCategoriasPorId(Guid id);

  }
}
