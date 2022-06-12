using EasyList.Business.Models;
using System;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IServices
{
  public interface ICategoriaService : IDisposable
  {
    Task<bool> Adicionar(Categoria categoria);
    Task<bool> Atualizar(Categoria categoria);
    Task<bool> Remover(Guid id);

    Task<bool> CategoriaExists(Guid Id);
  }
}
