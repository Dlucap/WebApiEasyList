using EasyList.Business.Interfaces;
using EasyList.Business.Models;
using EasyList.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyList.Data.Repository
{
  public class CompraRepository : Repository<Compra>, ICompraRepository
  {
    public CompraRepository(MeuDbContext context) :base(context)
    {

    }

    public async Task<Compra> ObterCompraPorId(Guid Id)
    {
      return await Db.Compra.AsNoTracking()
              .FirstOrDefaultAsync(c => c.Id == Id);
    }

    public async Task<Compra> ObterCompraPorData(DateTime dtCompra)
    {
      return await Db.Compra.AsNoTracking()
              .FirstOrDefaultAsync(c => c.DataModificacao == dtCompra);
    }
          
    //public  Task<Compra> ObterCompraPorPeriodoCompra(DateTime dtInicio, DateTime dtFim);
    //{
    //  return await Db.Compra.AsNoTracking()
    //      .FirstOrDefaultAsync(c => c.DataModificacao <= dtInicio
    //          && c.DataModificacao <= dtFim);
    //}
       
  }
}
