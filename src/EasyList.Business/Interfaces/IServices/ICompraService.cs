using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IServices
{
    public interface ICompraService :IDisposable
    {
        Task<bool> Adicionar(Compra compra);
        Task<bool> Atualizar(Compra compra);
        Task<bool> Remover(Guid id);

        Task<bool> CompraExists(Guid id);
        Task<decimal> CalculaValorTotalCompra(Guid id);
        Task<Compra> ObterPorId(Guid id);
        Task<IEnumerable<Compra>> ObterCompraPorPeriodoCompra(DateTime dtInicio, DateTime dtFim);
        Task<IEnumerable<Compra>> ObterTodasCompras();
        Task<IEnumerable<Compra>> ObterTodosPorPaginacao(int? pagina, int tamanho, bool ativo = false);


    }
}
