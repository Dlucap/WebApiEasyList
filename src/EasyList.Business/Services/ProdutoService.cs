using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Interfaces.IServices;
using EasyList.Business.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyList.Business.Services
{
    public class ProdutoService : IProdutoService
    {
        public readonly IProdutoRepository _produtoRepository;
        public readonly ICategoriaService _categoriaServices;

        public ProdutoService(IProdutoRepository produtoRepository, ICategoriaService categoriaServices)
        {
            _produtoRepository = produtoRepository;
            _categoriaServices = categoriaServices;
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

        public async Task<string> ImportarProdutos(IFormFile formFile, string user)
        {
            if (formFile.FileName.Split('.').Last() != "csv")
                throw new Exception("Please send an excel file to upload");

            var ms = new MemoryStream();
            formFile.CopyTo(ms);

            string result = Encoding.UTF8.GetString(ms.ToArray()).Trim();
            var produtos = result.Split(Environment.NewLine).Skip(1);

            var listaProdutosParaImportar = new List<Produto>();
            var logImportacao = new StringBuilder();
            var countSucess = 0;

            foreach (var produto in produtos)
            {
                var modelProduto = produto.Split(";");
                var categoriaId = Guid.Parse(modelProduto[0]);
               
                var categoriaExiste = await _categoriaServices.ObterPorId(categoriaId);

                if (categoriaExiste is null)
                {
                    logImportacao.AppendLine($" CategoriaId {categoriaId} não existe para o registro \"{produto}\".");
                    continue;
                }
                var nomeProduto = modelProduto[2];
                var produtoDescricao = modelProduto[3];
                var marca = modelProduto[1];

                var produtoExiste = await _produtoRepository.ObterProdutoPorNome(nomeProduto);
                if (produtoExiste is not null)
                {
                    logImportacao.AppendLine($" Produto já cadastrado na base de dados. Regisro: \"{produto}\".");
                    continue;
                }

                var ativo = Convert.ToBoolean(modelProduto[4]);
                var controlaEstoque = Convert.ToBoolean(modelProduto[5]);
                var novoProduto = new Produto
                {
                    //CategoriaId;Marca;Nome;Descricao;Ativo;ControlaEstoque
                    CategoriaId = categoriaId,
                    Marca = marca,
                    Nome = nomeProduto,
                    Descricao = produtoDescricao,
                    Ativo = ativo,
                    ControlaEstoque = controlaEstoque,
                    UsuarioCriacao = user,
                    UsuarioModificacao = user
                };

                listaProdutosParaImportar.Add(novoProduto);
                countSucess++;
            }

            if (listaProdutosParaImportar.Any())
                 await _produtoRepository.Adicionar(listaProdutosParaImportar);

            if(countSucess > 0)
            logImportacao.AppendLine($" - {countSucess} foram importado(s) com sucesso - ");

            return logImportacao.ToString();
        }

        public void Dispose()
        {
            _produtoRepository?.Dispose();
        }

    }
}
