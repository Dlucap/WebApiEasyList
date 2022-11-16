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
  public class FormaPagamentoRepository : Repository<FormaPagamento>, IFormaPagamentoRepository
  {
    public FormaPagamentoRepository(MeuDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<FormaPagamento>> ObterFormasPgamento(int? pagina, int tamanho, bool ativo)
    {
      if (tamanho > 15)
        tamanho = 15;

      return await DbSet.AsNoTracking()
                        .Where(e => e.Ativo == ativo)
                        .OrderBy(e => e.DataCriacao)
                        .Skip(tamanho * (pagina.Value - 1)).Take(tamanho)
                        .ToListAsync();
    }

    public async Task<FormaPagamento> ObterFormaPagamentoPorNome(string nomeFormaPagamento)
    {
      return await Db.FormaPagamento.AsNoTracking()
                     .FirstOrDefaultAsync(pg => pg.NomeFormaPagamento.Contains(nomeFormaPagamento));
    }

    public async override Task<FormaPagamento> ObterPorId(Guid id)
    {
      return await Db.FormaPagamento.AsNoTracking()
                                .FirstOrDefaultAsync(f => f.Id == id);
    }

  }
}
