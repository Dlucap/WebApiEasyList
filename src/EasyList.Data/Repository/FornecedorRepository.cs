using EasyList.Business.Interfaces;
using EasyList.Business.Models;
using EasyList.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EasyList.Data.Repository
{
  public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
  {
    public FornecedorRepository(MeuDbContext context) : base(context)
    {
    }

    public async Task<Fornecedor> ObterFornecedorPorId(Guid id)
    {
      return await Db.Fornecedor.AsNoTracking()
          .Include(f => f.Endereco)
          .FirstOrDefaultAsync(f => f.Id == id);
    }

  }
}
