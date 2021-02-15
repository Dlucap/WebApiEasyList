using Microsoft.EntityFrameworkCore;
using System;

namespace WebApiEasyList.Data
{
  public class CompraDbContext : DbContext
  {
    public CompraDbContext(DbContextOptions<CompraDbContext> options) : base(options)
    {

    }

    public DbSet<Compra> Compra { get; set; }
    public DbSet<ItmCompra> itmCompra { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<Compra>()
    .Property(p => p.Id)
     .ValueGeneratedOnAdd();

      modelBuilder.Entity<Compra>()
        .Property(p => p.IdFornecedor)
         .HasPrecision(5)
         .IsRequired();
      
      modelBuilder.Entity<Compra>()
          .Property(p => p.IdItmCompra)
         .HasPrecision(5)
         .IsRequired();

      modelBuilder.Entity<ItmCompra>()
      .Property(p => p.IdProduto)
        .HasPrecision(5);

      modelBuilder.Entity<ItmCompra>()
      .HasMany<Compra>(c => c.)
         .WithOne<ItmCompra>(itc => itc.Compra);


      modelBuilder.Entity<Compra>()
            .Property(p => p.DataCompra)
           .IsRequired();

    }

  }
}

/* 
 * https://www.devmedia.com.br/entity-framework-tutorial/27764
       * https://www.devmedia.com.br/entity-framework-tutorial/27764
       * https://www.entityframeworktutorial.net/efcore/configure-one-to-many-relationship-using-fluent-api-in-ef-core.aspx
         modelBuilder.Entity<ItmCompra>()
           .HasOne(c => c.Id)
         .Property(p => p.Id)
         .ValueGeneratedOnAdd()
         .HasPrecision(5);
//.IsRequired();
*/