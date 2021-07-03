﻿using EasyList.Business.Interfaces;
using EasyList.Business.Models;
using EasyList.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EasyList.Data.Repository
{
  public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
  {
    public FornecedorRepository(MeuDbContext context) : base(context)
    {
    }

    public async Task<Fornecedor> ObterFornecedorPorEndereco(Guid id)
    {
      return await Db.Fornecedor.AsNoTracking()
          .Include(c => c.Endereco)
          .FirstOrDefaultAsync(c => c.Id == id);
    }

  }
}