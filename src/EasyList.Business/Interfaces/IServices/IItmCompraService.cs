using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IServices
{
  public interface IItmCompraService : IDisposable
  {
    Task<bool> Adicionar(ItmCompra itmCompra);
    Task<bool> Atualizar(ItmCompra itmCompra);
    Task<bool> Remover(Guid id);
    Task<IList<ItmCompra>> BuscarItensCompra(Guid idCompra, string userName);
    Task<ItmCompra> ObterPorId(Guid id, Guid compraId);

  }
}
