using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using EasyList.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Data.Repository
{
  public class ProdutoRepository : Repository<Produto>, IProdutoRepository
  {
    public ProdutoRepository(MeuDbContext context) : base(context)
    {
    }
    public async Task<Produto> ObterProdutoPorNome(string nome)
    {
      return await Db.Produto.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.Nome.Contains(nome));
    }
    public async override Task<Produto> ObterPorId(Guid id)
    {
      return await Db.Produto.AsNoTracking()
                                .FirstOrDefaultAsync(p => p.Id == id);
    }

  }
}
