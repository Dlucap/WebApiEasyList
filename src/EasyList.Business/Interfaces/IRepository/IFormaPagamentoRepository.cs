using EasyList.Business.Models;
using System;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IRepository
{
  public interface IFormaPagamentoRepository : IRepository<FormaPagamento>
  {
    Task<FormaPagamento> ObterFormaPagamentoPorNome(string nomeFormaPagamento);

  }
}
