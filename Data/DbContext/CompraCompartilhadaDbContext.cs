using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiEasyList.Data
{
  public class CompraCompartilhadaDbContext : DbContext
  {
    public CompraCompartilhadaDbContext(DbContextOptions<CompraCompartilhadaDbContext> options) : base(options)
    {

    }

    public DbSet<CompraCompartilhada> CompraCompartilhada { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<CompraCompartilhada>()
       .Property(p => p.Id)
        .ValueGeneratedOnAdd();

      modelBuilder.Entity<CompraCompartilhada>()
     .Property(p => p.IdCompra)
      .IsRequired();

      modelBuilder.Entity<CompraCompartilhada>()
        .Property(p => p.UsuariosCompartilhados)
         .IsRequired();

      modelBuilder.Entity<CompraCompartilhada>()
          .Property(p => p.RecCreatedBy);

      modelBuilder.Entity<CompraCompartilhada>()
            .Property(p => p.RecCreatedOn);

      modelBuilder.Entity<CompraCompartilhada>()
            .Property(p => p.RecModifiedBy);

      modelBuilder.Entity<CompraCompartilhada>()
            .Property(p => p.RecModifiedOn);
    }
  }
}
