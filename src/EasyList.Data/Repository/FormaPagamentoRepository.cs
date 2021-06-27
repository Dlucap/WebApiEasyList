using EasyList.Business.Interfaces;
using EasyList.Business.Models;
using EasyList.Data.Context;
using Microsoft.EntityFrameworkCore;
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

    public bool FormaPagamentoExist(int idformaPagamento)
    {
      var produto = ObterFormaPagamentoPorId(idformaPagamento);

      if (produto != null)
        return true;
      else
        return false;
    }

    public async Task<FormaPagamento> ObterFormaPagamentoPorId(int idformaPagamento)
    {
      return await Db.FormaPagamento.AsNoTracking()         
          .FirstOrDefaultAsync(pg => pg.Id == idformaPagamento);
    }
  }
}
