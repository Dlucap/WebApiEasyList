using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyList.Business.Services
{
    public class ProdutoService : IProdutoService
    {
        public readonly IProdutoRepository _produtoRepository;

        public ProdutoService(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        public async Task<bool> Adicionar(Produto produto)
        {
            if (_produtoRepository.Buscar(p => p.Id == produto.Id).Result.Any())
                return false;

            await _produtoRepository.Adicionar(produto);
            return true;          
        }

        public async Task<bool> Atualizar(Produto produto)
        {
            if (!_produtoRepository.Buscar(p => p.Id == produto.Id).Result.Any())
                return false;

            await _produtoRepository.Atualizar(produto);
            return true;
        }
       
        public async Task<Produto> ObterPorId(Guid Id)
        {
            return await _produtoRepository.ObterPorId(Id);
        }

        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _produtoRepository.ObterTodos();
        }

        public async Task<IEnumerable<Produto>> ObterTodosPorPaginacao(int? pagina, int tamanho)
        {
            return await _produtoRepository.ObterTodosPorPaginacao(pagina, tamanho);
        }

        public async Task<bool> Remover(Guid id)
        {
            if (!_produtoRepository.Buscar(p => p.Id == id).Result.Any())
                return false;

            await _produtoRepository.Remover(id);
            return true;
        }

        public async Task<bool> Buscar(Guid id)
        {
            return _produtoRepository.Buscar(x => x.Id == id).Result.Any();
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }
               
    }
}
