﻿using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using EasyList.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EasyList.Data.Repository
{
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
  {
    public EnderecoRepository(MeuDbContext context) : base(context) { }

    public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
    {
      return await Db.Endereco.AsNoTracking()
                              .FirstOrDefaultAsync(e => e.FornecedorId == fornecedorId);
    }

  }
}
