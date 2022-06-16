using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyList.Business.Services
{
    public class FornecedorService : IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IEnderecoRepository _enderecoService;

        public FornecedorService(IFornecedorRepository fornecedorRepository,
                                 IEnderecoRepository enderecoService)
        {
            _fornecedorRepository = fornecedorRepository;
            _enderecoService = enderecoService;
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
            if (!_fornecedorRepository.Buscar(f => f.Cnpj == fornecedor.Cnpj && f.Id == fornecedor.Id).Result.Any())         
                return false;         

            await _fornecedorRepository.Atualizar(fornecedor);
            return true;
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            await _enderecoService.Atualizar(endereco);
        }

        public async Task<bool> Remover(Guid id)
        {
            if(!await FornecedorExists(id))
                return false;

            var fornecedor = await _fornecedorRepository.ObterPorId(id);

            fornecedor.Ativo = false;

            await _fornecedorRepository.Atualizar(fornecedor);
            return true;
        }

        public async Task<IEnumerable<Fornecedor>> ObterTodosPorPaginacao(int? pagina, int tamanho = 15, bool ativo = false)
        {
            return await _fornecedorRepository.ObterTodosPorPaginacao(pagina, tamanho, ativo);
        }

        public async Task<Fornecedor> ObterFornecedorEndereco(Guid id)
        {
            return await _fornecedorRepository.ObterFornecedorEndereco(id);
        }

        public async Task<IEnumerable<Fornecedor>> ObterTodosFornecedoresEndereco()
        {
            return await _fornecedorRepository.ObterTodosFornecedoresEndereco();
        }

        public async Task<bool> FornecedorExists(Guid id)
        {
            return _fornecedorRepository.Buscar(f => f.Id == id).Result.Any();
        }

        public void Dispose()
        {
            _enderecoService?.Dispose();
            _fornecedorRepository?.Dispose();
        }
    }

}

