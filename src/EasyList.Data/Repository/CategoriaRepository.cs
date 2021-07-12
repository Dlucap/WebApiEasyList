using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using EasyList.Data.Context;

namespace EasyList.Data.Repository
{
  public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
  {
    public CategoriaRepository(MeuDbContext context) : base(context)
    {
    }
       
  }
}
