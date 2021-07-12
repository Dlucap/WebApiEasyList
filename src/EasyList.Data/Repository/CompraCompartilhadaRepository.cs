using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using EasyList.Data.Context;
using EasyList.Data.Repository;

namespace EasyList.Business.Interfaces
{
  public class CompraCompartilhadaRepository : Repository<CompraCompartilhada>, ICompraCompartilhadaRepository
  {
    public CompraCompartilhadaRepository(MeuDbContext context) : base(context)
    {
    }
       
  }
}
