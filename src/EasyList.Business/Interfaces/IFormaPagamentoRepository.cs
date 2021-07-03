using EasyList.Business.Intefaces;
using EasyList.Business.Models;
using System;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces
{
  public interface IFormaPagamentoRepository : IRepository<FormaPagamento>
  {
    Task<FormaPagamento> ObterFormaPagamentoPorNome(string nomeFormaPagamento);

    Task<FormaPagamento> ObterFormaPagamentoPorId(Guid idformaPagamento);

    bool FormaPagamentoExist(Guid idformaPagamento);

  }
}
