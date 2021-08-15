using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EasyList.Business.Services
{
  public class ItmCompraService : BaseService, IItmCompraService
  {

    private readonly IItmCompraRepository _itmCompraRepository;

    public ItmCompraService(IItmCompraRepository itmCompraRepository)
    {
      _itmCompraRepository = itmCompraRepository;
    }

    public async Task<bool> Adicionar(ItmCompra itmCompra)
    {
      if (_itmCompraRepository.Buscar(itmComp => itmComp.Id == itmCompra.Id).Result.Any())
        return false;

      await _itmCompraRepository.Adicionar(itmCompra);
      return true;
    }

    public async Task<bool> Atualizar(ItmCompra itmCompra)
    {
      if (_itmCompraRepository.Buscar(itmComp => itmComp.Id == itmCompra.Id).Result.Any())
        return false;
      
      await _itmCompraRepository.Atualizar(itmCompra);
      return true;
    }
     
    public async Task<bool> Remover(Guid id)
    {
      var itmCompra = await _itmCompraRepository.ObterPorId(id);
      if (itmCompra != null)
        await _itmCompraRepository.Remover(id);
            
      return true;
    }

    public void Dispose()
    {
      _itmCompraRepository?.Dispose();
    }
  }
}