using EasyList.Business.Models;
using System;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces
{
  public interface IFornecedorService : IDisposable
  {
    Task<bool> Adicionar(Fornecedor fornecedor);
    Task<bool> Atualizar(Fornecedor fornecedor);
    Task<bool> Remover(int id);

    Task AtualizarEndereco(Endereco endereco);
  }
}