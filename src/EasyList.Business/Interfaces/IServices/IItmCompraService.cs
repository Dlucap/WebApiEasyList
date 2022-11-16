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

    Task<IEnumerable<ItmCompra>> BuscarItensCompra(Guid idCompra, string userName);
    Task<ItmCompra> ObterPorId(Guid id, Guid compraId);
    Task<bool> ItensCompraExists(Guid id, Guid compraId);
    Task<IEnumerable<ItmCompra>> ObterTodosPorPaginacao(int? pagina, int tamanho = 15, bool ativo = false);

  }
}
