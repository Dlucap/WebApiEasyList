using EasyList.Business.Interfaces.IRepository;
using EasyList.Business.Models;
using EasyList.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EasyList.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, new()
    {
        protected readonly MeuDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected Repository(MeuDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<TEntity>> ObterTodos()
        {
            return await DbSet.AsNoTracking()
                              .OrderBy(e => e.DataCriacao)
                              .ToListAsync();
        }

        public virtual async Task<IEnumerable<TEntity>> ObterTodosPorPaginacao(int? pagina, int tamanho = 15, bool ativo = false)
        {
            if (tamanho > 15)
                tamanho = 15;

            return await DbSet.AsNoTracking()
                              .OrderBy(e => e.DataCriacao)
                              .Skip(tamanho * (pagina.Value - 1)).Take(tamanho)
                              .ToListAsync();
        }

        public abstract Task<Paginado> ObterTodosPorPaginacaoNovo(int? pagina, int tamanho = 20, bool ativo = false);
       
        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Adicionar(List<TEntity> entity)
        {
            DbSet.AddRange(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(List<TEntity> entity)
        {
            DbSet.UpdateRange(entity);
            await SaveChanges();
        }

        public virtual async Task Remover(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

    }
}
