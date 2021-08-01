using EasyList.Business.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EasyList.Data.Context
{
  public class MeuDbContext : DbContext
  {
    public MeuDbContext(DbContextOptions<MeuDbContext> options) : base(options) { }

    #region DBSet
    public DbSet<Categoria> Categoria { get; set; }
    public DbSet<Compra> Compra { get; set; }
    public DbSet<CompraCompartilhada> CompraCompartilhada { get; set; }
    public DbSet<Endereco> Endereco { get; set; }
    //public DbSet<Estoque> Estoque { get; set; }
    public DbSet<FormaPagamento> FormaPagamento { get; set; }
    public DbSet<Fornecedor> Fornecedor { get; set; }
    public DbSet<ItmCompra> ItmCompra { get; set; }
    public DbSet<Produto> Produto { get; set; }
    #endregion DBSet

    protected override void OnModelCreating(ModelBuilder builder)
    {
      foreach (var property in builder.Model.GetEntityTypes()
          .SelectMany(e => e.GetProperties()
              .Where(p => p.ClrType == typeof(string))))
        property.SetColumnType("varchar(100)");

      builder.ApplyConfigurationsFromAssembly(typeof(MeuDbContext).Assembly);

      foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

      base.OnModelCreating(builder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
      foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCriacao") != null))
      {
        if (entry.State == EntityState.Added)
        {        
          entry.Property("DataCriacao").CurrentValue = DateTime.Now;
          entry.Property("DataModificacao").CurrentValue = DateTime.Now;
        }
      }

      foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataModificacao") != null))
      {
        if (entry.State == EntityState.Modified)
        {
          entry.Property("DataModificacao").CurrentValue = DateTime.Now;
        }
      }

      return base.SaveChangesAsync(cancellationToken);
    }

  }
}

