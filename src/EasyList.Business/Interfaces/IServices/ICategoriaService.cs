using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IServices
{
 public interface ICategoriaService : IDisposable
  {
    Task<bool> CategoriaExists(Guid Id);
  }
}
