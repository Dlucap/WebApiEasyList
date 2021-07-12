﻿using EasyList.Business.Models;
using System;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IRepository
{
  public interface IProdutoRepository : IRepository<Produto>
  {
    Task<Produto> ObterProdutoPorNome(string nome);

    bool ProdutoExist(Guid id);
  }
}