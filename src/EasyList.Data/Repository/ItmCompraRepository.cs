using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using EasyList.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyList.Data.Repository
{
  public class ItmCompraRepository : Repository<ItmCompra>, IItmCompraRepository
  {
    public ItmCompraRepository(MeuDbContext context) : base(context)
    {

    }
  }
}
