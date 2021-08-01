using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using EasyList.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EasyList.Data.Repository
{
  public class FormaPagamentoRepository : Repository<FormaPagamento>, IFormaPagamentoRepository
  {
    public FormaPagamentoRepository(MeuDbContext context) : base(context)
    {
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
