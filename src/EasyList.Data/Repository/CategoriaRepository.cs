using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using EasyList.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
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

    public virtual async Task<IEnumerable<Categoria>> ObterTodosPorPaginacao(int? pagina, int tamanho = 15, bool ativo = false)
    {
      if (tamanho > 15)
        tamanho = 15;

      return await DbSet.AsNoTracking()                       
                        .Where(e => e.Ativo == ativo)
                        .OrderBy(e => e.DataCriacao)
                        .Skip(tamanho * (pagina.Value - 1)).Take(tamanho)
                        .ToListAsync();
    }
  }
}
