using EasyList.Business.Models;
using System;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IServices
{
  public interface IItmCompraService : IDisposable
  {
    Task<bool> Adicionar(ItmCompra fornecedor);
    Task<bool> Atualizar(ItmCompra fornecedor);
    Task<bool> Remover(Guid id);

  }
}
