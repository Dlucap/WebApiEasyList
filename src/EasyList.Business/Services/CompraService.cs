using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyList.Business.Services
{
    public class CompraService : ICompraService
    {
        public readonly ICompraRepository _compraRepository;

        public CompraService(ICompraRepository compraRepository)
        {
            _compraRepository = compraRepository;
        }

        public async Task<bool> Adicionar(Compra compra)
        {
            if (await CompraExists(compra.Id))
                return false;

            await _compraRepository.Adicionar(compra);
            return true;
        }     

        public async Task<bool> Atualizar(Compra compra)
        {
            if (!await CompraExists(compra.Id))
                return false;

            await _compraRepository.Atualizar(compra);
            return true;
        }
        
        public async Task<bool> Remover(Guid id)
        {
            if (!await CompraExists(id))
                return false;

            await _compraRepository.Remover(id);
            return true;
        }

        public async Task<bool> CompraExists(Guid id)
        {
            return _compraRepository.Buscar(c => c.Id == id).Result.Any();
        }          

        public async Task<decimal> CalculaValorTotalCompra(Guid id)
        {     
           return await _compraRepository.CalculaValorTotalCompra(id);
        }              

        public async Task<Compra> ObterPorId(Guid id)
        {
            return await _compraRepository.ObterPorId(id);
        }

        public async Task<IEnumerable<Compra>> ObterCompraPorPeriodoCompra(DateTime dtInicio, DateTime dtFim)
        {
            return await _compraRepository.ObterCompraPorPeriodoCompra(dtInicio, dtFim);
        }

        public async Task<IEnumerable<Compra>> ObterTodasCompras()
        {
            return await _compraRepository.ObterTodos();
        }

        public async Task<IEnumerable<Compra>> ObterTodosPorPaginacao(int? pagina, int tamanho, bool ativo = false)
        {
            return await _compraRepository.ObterTodosPorPaginacao(pagina,tamanho,ativo);
        }

        public void Dispose()
        {
            _compraRepository?.Dispose();
        }
    }
}
