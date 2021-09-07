using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EasyList.Business.Services
{
  public class CategoriaService : BaseService, ICategoriaService
  {
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
      _categoriaRepository = categoriaRepository;
    }

    public async Task<bool> CategoriaExists(Guid Id)
    {
       return  _categoriaRepository.Buscar(cat => cat.Id == Id).Result.Any();
    }

    public void Dispose()
    {
      _categoriaRepository?.Dispose();
    }
  }
}
