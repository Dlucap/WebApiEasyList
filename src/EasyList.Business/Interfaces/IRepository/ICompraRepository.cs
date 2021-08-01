using EasyList.Business.Models;

namespace EasyList.Business.Interfaces.IRepository
{
  public interface ICompraRepository : IRepository<Compra>
  {  
    //Task<Compra> ObterCompraPorData(DateTime dtCompra);

    //Task<Compra> ObterCompraPorPeriodoCompra(DateTime dtInicio, DateTime dtFim);

  }
}
