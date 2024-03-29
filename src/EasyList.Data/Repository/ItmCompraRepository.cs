﻿using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using EasyList.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyList.Data.Repository
{
  public class ItmCompraRepository : Repository<ItmCompra>, IItmCompraRepository
  {
    public ItmCompraRepository(MeuDbContext context) : base(context)
    {

    }

    public async Task<ItmCompra> ObterPorId(Guid id, Guid compraId)
    {
      return await DbSet.FirstOrDefaultAsync(itm => itm.Id == id && itm.CompraId == compraId);
    }

    public virtual async Task<IEnumerable<ItmCompra>> ObterTodosPorPaginacao(int? pagina, int tamanho = 15, bool ativo = false)
    {
      if (tamanho > 15)
        tamanho = 15;

      return await DbSet.AsNoTracking()
                        .Where(e => e.Ativo == ativo)
                        .OrderBy(e => e.DataCriacao)
                        .Skip(tamanho * (pagina.Value - 1)).Take(tamanho)
                        .ToListAsync();
    }

  }
}
