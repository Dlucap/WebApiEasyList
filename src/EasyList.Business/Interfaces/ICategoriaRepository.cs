using EasyList.Business.Intefaces;
using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces
{
  public class ICategoriaRepository : IRepository<Categoria>
  {

    public Task Adicionar(Categoria entity)
    {
      throw new NotImplementedException();
    }

    public Task Atualizar(Categoria entity)
    {
      throw new NotImplementedException();
    }

    public Task<IEnumerable<Categoria>> Buscar(Expression<Func<Categoria, bool>> predicate)
    {
      throw new NotImplementedException();
    }

    public void Dispose()
    {
      throw new NotImplementedException();
    }

    public Task<Categoria> ObterPorId(Guid id)
    {
      throw new NotImplementedException();
    }

    public Task<List<Categoria>> ObterTodos()
    {
      throw new NotImplementedException();
    }

    public Task Remover(Guid id)
    {
      throw new NotImplementedException();
    }

    public Task<int> SaveChanges()
    {
      throw new NotImplementedException();
    }
  }
}
