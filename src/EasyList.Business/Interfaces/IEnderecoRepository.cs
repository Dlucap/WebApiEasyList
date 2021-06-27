using EasyList.Business.Intefaces;
using EasyList.Business.Models;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces
{
  public interface IEnderecoRepository : IRepository<Endereco>
  {
    Task<Endereco> ObterEnderecoPorFornecedor(int fornecedorId);
  }
}
