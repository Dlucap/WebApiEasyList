using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using EasyList.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyList.Data.Repository
{
    public class CompraRepository : Repository<Compra>, ICompraRepository
    {
        public CompraRepository(MeuDbContext context) : base(context)
        {

        }

        public override async Task Adicionar(Compra compra)
        {
            try
            {
                Db.Database.BeginTransaction();
                Db.Compra.Add(compra);
                Db.SaveChanges();
                Db.Database.CommitTransaction();
            }
            catch (Exception ex)
            {
                Db.Database.RollbackTransaction();
            }
        }

        public async Task<IEnumerable<Compra>> ObterTodasCompras()
        {
            return await Db.Compra.AsNoTracking()
                              .Include(itm => itm.ItemsCompra)
                             .OrderBy(e => e.DataCriacao)
                             .ToListAsync();
        }

        public async Task<Compra> ObterCompraPorData(DateTime dtCompra)
        {
            return await Db.Compra.AsNoTracking()
                    .Include(itm => itm.ItemsCompra)
                    .FirstOrDefaultAsync(c => c.DataModificacao == dtCompra);
        }

        public async override Task<Compra> ObterPorId(Guid id)
        {
            return await Db.Compra.AsNoTracking()
                      .Include(itm => itm.ItemsCompra)
                      .FirstOrDefaultAsync(comp => comp.Id == id);
        }

        public async Task<decimal> CalculaValorTotalCompra(Guid id)
        {
            if (!Buscar(c => c.Id == id).Result.Any())
                return -1;

            var itens = await Db.ItmCompra.AsNoTracking()
                                .Where(it => it.CompraId == id)
                                .Select(itm => new ItmCompra
                                {
                                    Quantidade = itm.Quantidade,
                                    Preco = itm.Preco
                                })
                                .ToListAsync();
            decimal result = 0;

            foreach (var item in itens)
                result = result + (item.Quantidade * item.Preco);

            return Convert.ToDecimal(result.ToString("F"));

        }

        public async Task<IEnumerable<Compra>> ObterCompraPorPeriodoCompra(DateTime dtInicio, DateTime dtFim)
        {
            return Db.Compra.AsNoTracking()
                     .Include(itm => itm.ItemsCompra)
                     .Where(c => c.DataCompra >= dtInicio && c.DataCompra <= dtFim);
        }        
    }
}
