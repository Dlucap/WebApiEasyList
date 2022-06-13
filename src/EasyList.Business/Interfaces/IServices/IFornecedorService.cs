using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IServices
{
  public interface IFornecedorService : IDisposable
  {
    Task<bool> Adicionar(Fornecedor fornecedor);
    Task<bool> Atualizar(Fornecedor fornecedor);
    Task<bool> Remover(Guid id);
    Task<IEnumerable<Fornecedor>> ObterTodosFornecedoresEndereco();
    Task<IEnumerable<Fornecedor>> ObterTodosPorPaginacao(int? pagina, int tamanho = 15, bool ativo = false);   
    Task AtualizarEndereco(Endereco endereco);
    Task<IEnumerable<Fornecedor>> ObterFornecedoresEndereco(Guid id);
    Task<bool> FornecedorExists(Guid id);    
  }
}