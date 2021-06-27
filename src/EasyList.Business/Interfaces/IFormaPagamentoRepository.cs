using EasyList.Business.Intefaces;
using EasyList.Business.Models;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces
{
  public interface IFormaPagamentoRepository : IRepository<FormaPagamento>
  {
    Task<FormaPagamento> ObterFormaPagamentoPorNome(string nomeFormaPagamento);

    Task<FormaPagamento> ObterFormaPagamentoPorId(int idformaPagamento);

    bool FormaPagamentoExist(int idformaPagamento);

  }
}
