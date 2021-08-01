using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using EasyList.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EasyList.Data.Repository
{
  public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
  {
    public CategoriaRepository(MeuDbContext context) : base(context)
    {
    }

    public async override Task<Categoria> ObterPorId(Guid id)
    {
      return await Db.Categoria.AsNoTracking()
                                .FirstOrDefaultAsync(cat => cat.Id == id);
    }
  }
}
