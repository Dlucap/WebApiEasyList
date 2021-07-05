using EasyList.Business.Intefaces;
using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces
{
 public interface ICompraRepository : IRepository<Compra>
  {
    Task<Compra> ObterCompraPorId(Guid id);

    Task<Compra> ObterCompraPorData(DateTime dtCompra);

    //Task<Compra> ObterCompraPorPeriodoCompra(DateTime dtInicio, DateTime dtFim);

  }
}
