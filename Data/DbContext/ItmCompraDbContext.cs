using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiEasyList.Data
{
  public class ItmCompraDbContext : DbContext
  {

    public ItmCompraDbContext(DbContextOptions<ItmCompraDbContext> options) : base(options)
    {

    }

    public DbSet<ItmCompra> ItmCompra { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.Entity<ItmCompra>()
    .Property(p => p.Id)
     .ValueGeneratedOnAdd();

      modelBuilder.Entity<ItmCompra>()
        .Property(p => p.IdCompra)
         .HasPrecision(5);

      modelBuilder.Entity<ItmCompra>()
      .Property(p => p.IdProduto)
        .HasPrecision(5);
    }
  }
}
