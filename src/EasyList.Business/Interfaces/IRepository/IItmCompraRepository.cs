using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IRepository
{
  public interface IItmCompraRepository : IRepository<ItmCompra>
  {
    Task<ItmCompra> ObterPorId(Guid id, Guid compraId);

    //Task<Compra> ObterCompraPorData(DateTime dtCompra);

    //Task<Compra> ObterCompraPorPeriodoCompra(DateTime dtInicio, DateTime dtFim);

  }
}
