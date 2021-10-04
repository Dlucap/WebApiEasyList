using EasyList.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EasyList.Business.Interfaces.IRepository
{
  public interface IRepository<TEntity> : IDisposable where TEntity : Entity
  {
    Task Adicionar(TEntity entity);
    Task<TEntity> ObterPorId(Guid id);
    Task<List<TEntity>> ObterTodos();
    Task<List<TEntity>> ObterTodosPorPaginacao(int? pagina, int tamanho = 15);
    Task Atualizar(TEntity entity);
    Task Remover(Guid id);
    Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate);
    Task<int> SaveChanges();
  }
}
