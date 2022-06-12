using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EasyList.Business.Services
{
  public class CategoriaService : ICategoriaService
  {
    private readonly ICategoriaRepository _categoriaRepository;

    public CategoriaService(ICategoriaRepository categoriaRepository)
    {
      _categoriaRepository = categoriaRepository;
    }

    public async Task<bool> Adicionar(Categoria categoria)
    {
      if (!await CategoriaExists(categoria.Id)) 
        return false;

      await _categoriaRepository.Adicionar(categoria);
      return true;
    }

    public async Task<bool> Atualizar(Categoria categoria)
    {

      if (!await CategoriaExists(categoria.Id))
        return false;

      await _categoriaRepository.Atualizar(categoria);

      return true;
    }

    public async Task<bool> CategoriaExists(Guid Id)
    {
       return  _categoriaRepository.Buscar(cat => cat.Id == Id).Result.Any();
    }

    public async Task<bool> Remover(Guid id)
    {
      var itmCompra = await _categoriaRepository.ObterPorId(id);
      if (itmCompra != null)
        await _categoriaRepository.Remover(id);

      return true;
    }

    public void Dispose()
    {
      _categoriaRepository?.Dispose();
    }
  }
}
