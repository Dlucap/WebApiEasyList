using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using EasyList.Data.Context;
using EasyList.Data.Repository;
using Microsoft.EntityFrameworkCore;

namespace EasyList.Data.Repository
{
  public class CompraCompartilhadaRepository : Repository<CompraCompartilhada>, ICompraCompartilhadaRepository
  {
    public CompraCompartilhadaRepository(MeuDbContext context) : base(context)
    {
      /**
       * Desabilitando o rastreamento a nível de instância de contexto / EF Core - Desabilitando o rastreamento de consultas (No Tracking)
       * http://www.macoratti.net/18/04/efcore_notrack1.htm
       **/
      context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
    }
       
  }
}
