using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace EasyList.Business.Services
{
  public class FornecedorService : BaseService, IFornecedorService
  {
    private readonly IFornecedorRepository _fornecedorRepository;
    private readonly IEnderecoRepository _enderecoRepository;

    public FornecedorService(IFornecedorRepository fornecedorRepository,
                             IEnderecoRepository enderecoRepository)
    {
      _fornecedorRepository = fornecedorRepository;
      _enderecoRepository = enderecoRepository;
    }

    public async Task<bool> Adicionar(Fornecedor fornecedor)
    {
      if (_fornecedorRepository.Buscar(f => f.Cnpj == fornecedor.Cnpj).Result.Any())
      {
        return false;
      }

      await _fornecedorRepository.Adicionar(fornecedor);
      return true;
    }

    public async Task<bool> Atualizar(Fornecedor fornecedor)
    {
      if (_fornecedorRepository.Buscar(f => f.Cnpj == fornecedor.Cnpj && f.Id == fornecedor.Id).Result.Any())
      {
        return false;
      }

      await _fornecedorRepository.Atualizar(fornecedor);
      return true;
    }

    public async Task AtualizarEndereco(Endereco endereco)
    {
      await _enderecoRepository.Atualizar(endereco);
    }

    public async Task<bool> Remover(Guid id)
    {
      var endereco = await _enderecoRepository.ObterEnderecoPorFornecedor(id);
      if (endereco != null)
        await _enderecoRepository.Remover(id);

      await _fornecedorRepository.Remover(id);
      return true;
    }
    
    public void Dispose()
    {
      _enderecoRepository?.Dispose();
      _fornecedorRepository?.Dispose();
    }

  }
}
