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
  public class CompraRepository : Repository<Compra>, ICompraRepository
  {
    public CompraRepository(MeuDbContext context) : base(context)
    {

    }


    public async Task<IEnumerable<Compra>> ObterTodasCompras()
    {
      return await Db.Compra.AsNoTracking()
                        .Include(itm => itm.ItemsCompra)
                       .OrderBy(e => e.DataCriacao)
                       .ToListAsync();
    }

    public async Task<Compra> ObterCompraPorData(DateTime dtCompra)
    {
      return await Db.Compra.AsNoTracking()
              .Include(itm => itm.ItemsCompra)
              .FirstOrDefaultAsync(c => c.DataModificacao == dtCompra);
    }

    public async override Task<Compra> ObterPorId(Guid id)
    {
      return await Db.Compra.AsNoTracking()
                .Include(itm => itm.ItemsCompra)
                .FirstOrDefaultAsync(comp => comp.Id == id);
    }

    //public Task<Compra> ObterCompraPorPeriodoCompra(DateTime dtInicio, DateTime dtFim);
    //  {
    //    return await Db.Compra.AsNoTracking()
    //        .FirstOrDefaultAsync(c => c.DataModificacao <= dtInicio
    //            && c.DataModificacao <= dtFim);
    //}

  }
}
