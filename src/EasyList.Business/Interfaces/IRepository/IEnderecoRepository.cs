using EasyList.Business.Models;
using System;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IRepository
{
  public interface IEnderecoRepository : IRepository<Endereco>
  {
    Task<Endereco> ObterEnderecoPorFornecedor(Guid enderecoId);
  }
}
