using EasyList.Business.Intefaces;
using EasyList.Business.Models;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces
{
  public interface IFornecedorRepository : IRepository<Fornecedor>
  {
    Task<Fornecedor> ObterFornecedorPorEndereco(int id);
  
  }
}
