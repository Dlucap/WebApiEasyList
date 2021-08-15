using EasyList.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IRepository
{
  public interface ICompraRepository : IRepository<Compra>
  {

    Task<IEnumerable<Compra>> ObterTodasCompras();

    //Task<Compra> ObterCompraPorData(DateTime dtCompra);

    //Task<Compra> ObterCompraPorPeriodoCompra(DateTime dtInicio, DateTime dtFim);

  }
}
