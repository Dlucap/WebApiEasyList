using EasyList.Business.Intefaces;
using EasyList.Business.Models;
using System;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces
{
  public interface IEnderecoRepository : IRepository<Endereco>
  {
    Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId);
  }
}
