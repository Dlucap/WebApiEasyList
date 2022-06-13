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
  public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
  {
    public FornecedorRepository(MeuDbContext context) : base(context)
    {
    }
       
    public virtual async Task<IList<Fornecedor>> ObterTodosPorPaginacao(int? pagina, int tamanho = 15, bool ativo =false)
    {
      if (tamanho > 15)
          tamanho = 15;

        return await DbSet.AsNoTracking()
                          .Include(c => c.Endereco)
                          .Where(e => e.Ativo == ativo)
                          .OrderBy(e => e.DataCriacao)
                          .Skip(tamanho * (pagina.Value - 1)).Take(tamanho)
                          .ToListAsync();      
    }

    public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
    {
      return await Db.Fornecedor.AsNoTracking()
          .Include(c => c.Endereco)
          .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id)
    {
      return await Db.Fornecedor.AsNoTracking()
          .Include(c => c.Endereco).Include(c => c.)
          .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<IEnumerable<Fornecedor>> ObterTodosFornecedoresEndereco()
    {
      return await Db.Fornecedor.AsNoTracking()
         .Include(c => c.Endereco)
         .ToListAsync();
    }    
  }
}
