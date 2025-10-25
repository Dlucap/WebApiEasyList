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
        private readonly ILogService _logService;

        public CompraService(ICompraRepository compraRepository, ILogService logService)
        {
            _compraRepository = compraRepository;
            _logService = logService;
        }

        public async Task<bool> Adicionar(Compra compra)
        {
            if (await CompraExists(compra.Id))
            {
                await _logService.LogAviso(
                    $"Tentativa de adicionar Compra com ID já existente: {compra.Id}",
                    "Database",
                    $"Usuário: {compra.UsuarioCriacao}"
                );
                return false;
            }

            await _compraRepository.Adicionar(compra);
            await _logService.LogOperacaoBancoDados("INSERT", "Compra", $"ID: {compra.Id}, Usuário: {compra.UsuarioCriacao}");
            return true;
        }     

        public async Task<bool> Atualizar(Compra compra)
        {
            if (!await CompraExists(compra.Id))
            {
                await _logService.LogAviso(
                    $"Tentativa de atualizar Compra inexistente: {compra.Id}",
                    "Database",
                    $"Usuário: {compra.UsuarioModificacao}"
                );
                return false;
            }

            await _compraRepository.Atualizar(compra);
            await _logService.LogOperacaoBancoDados("UPDATE", "Compra", $"ID: {compra.Id}, Usuário: {compra.UsuarioModificacao}");
            return true;
        }
        
        public async Task<bool> Remover(Guid id)
        {
            if (!await CompraExists(id))
            {
                await _logService.LogAviso(
                    $"Tentativa de remover Compra inexistente: {id}",
                    "Database"
                );
                return false;
            }

            await _compraRepository.Remover(id);
            await _logService.LogOperacaoBancoDados("DELETE", "Compra", $"ID: {id}");
            return true;
        }

        public async Task<bool> CompraExists(Guid id)
        {
            var compra = await _compraRepository.Buscar(c => c.Id == id);

            return compra.Any()
;        }          

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
